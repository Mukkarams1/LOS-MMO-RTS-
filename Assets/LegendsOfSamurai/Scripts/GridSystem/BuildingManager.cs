using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using System.Linq;
//using static UnityEditor.PlayerSettings;

public class BuildingManager : MonoBehaviour
{
    // [SerializeField] TileMapper tileMapper;
    string SelectedBuildingId;
    public string SelectedBuildingidForClick;
    public string SelectedBuildingUniqueId;
    string BuildingName;
    int SelectedBuildingSizeX;
    int SelectedBuildingSizeY;
    BuildingsData selectedBuildingData;
    Building selectedBuildingUniqueData;
    GameObject instantiatedBuilding;
    [SerializeField] GameObject gridParent;
    GameObject tile;
    Dictionary<Vector2, GridCell> tilesDictionary = new Dictionary<Vector2, GridCell>();
    public UpgradePanelUI upgradePanel;

    public bool isConstructMode = false;
    public bool CanBeConstructed = true;
    public Vector2Int CurrentPos;
    //public static BuildingManager instance;
    public Player player;
    GridSystem grid;

    public List<BuildingPrefab> buildingPrefabsList = new List<BuildingPrefab>();

    public List<GameObject> GridGameObjects = new List<GameObject>();
    internal void onBuildingIntracted(Building currentBuildingData, BuildingPrefab building, int category)
    {
        SelectedBuildingidForClick = currentBuildingData.buildingId;
        SelectedBuildingUniqueId = currentBuildingData._id;
        GameManager.instance.ToggleBuildingButtonsPanel(true);
        GameManager.instance.buildingButtonsPanel.setOnClickBtns(building, category);

    }

    internal void onConfirmClicked(BuildingsData building)
    {
        Debug.Log("currentPos" + CurrentPos);
        Debug.Log("currentPosIsValid" + grid.isCellValid(CurrentPos));
        if (grid.isCellValid(CurrentPos) == true)
        {
            CanBeConstructed = true;
            ConfirmPlaceObject();
        }
        else
        {
            var currentBuilding = instantiatedBuilding.GetComponent<BuildingPrefab>();
            currentBuilding.Cancel();
        }

    }

    void Start()
    {
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }


        grid = new GridSystem(32, 32);
        grid.onCellCreated += CreatTileOnGrid;
        grid.onCellOcupied += SetTileColor;
        grid.InitialiseGrid();
        GameManager.instance.inputManager.onCancel += onBuildingCancel;
        tile = Resources.Load<GameObject>("Prefabs/Tile");
        player = new Player();
        player.Initialize();
        //  UpdateResourcesAndTime();
        //  RefreshAllBuildingsStates(DataManager.instance.GetUserBuildingsData());
        //  GenerateGridTiles();
    }

    private void SetTileColor(int arg1, int arg2)
    {
        foreach(var grid in GridGameObjects)
        {
            if(grid.name == arg1 + ", " + arg2)
            {
                grid.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
            }
        }
    }

    public void UpdateResourcesAndTime()
    {
        ApiController.ResourceAndTimeUpdateAPI(DataManager.instance.UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken);
    }

    public void UpdateResourcesAndTimeWithDelay()
    {
        StartCoroutine(WaitCallResourcesApi());
    }
    public IEnumerator WaitCallResourcesApi()
    {
        yield return new WaitForSeconds(3);
        UpdateResourcesAndTime();
    }

    private void Update()
    {
        //Debug.Log("Building id is" + SelectedBuildingidForClick);
    }
    private void OnDisable()
    {
        GameManager.instance.inputManager.onCancel -= onBuildingCancel;
        // grid.onCellCreated -= CreatTileOnGrid;
    }
    public void ConfirmPlaceObject()
    {
        if (instantiatedBuilding != null && grid.isCellValid(CurrentPos) == true)
        {
            var building = instantiatedBuilding.GetComponent<BuildingPrefab>();
            //instantiatedBuilding.GetComponent<BuildingPrefab>().buildingManager = GetComponent<BuildingManager>();
            building.GetCurrentPos();
            building.Isplaced = true;
            BuildingController entity = new BuildingController(SelectedBuildingId, CurrentPos, new Vector2Int(selectedBuildingData.xSize, selectedBuildingData.ySize));
            grid.CreatObject(entity, CurrentPos, new Vector2Int(selectedBuildingData.xSize, selectedBuildingData.ySize));
            building.CloseTickCrossPanel();
            InstantUpgradeBuildingState(SelectedBuildingId, EnumBuildingStates.construct);
            print("Current pos" + CurrentPos);
            isConstructMode = false;
        }

    }

    internal bool IsConstructable(Vector2Int vector2Int1, Vector2Int vector2Int2)
    {
        return grid.IsConstructable(vector2Int1, vector2Int2);
    }

    void ConstructCallBack(bool status, String response)
    {
        Debug.Log(response);
        if (!status)
        {
            Destroy(instantiatedBuilding);
            buildingPrefabsList.Remove(buildingPrefabsList.Last());
            return;
        }
        else
        {
            SignInResponse ConstructionApiResponseData = new SignInResponse();
            ConstructionApiResponseData = JsonConvert.DeserializeObject<SignInResponse>(response);
            instantiatedBuilding.GetComponent<BuildingPrefab>().CurrentBuildingData = ConstructionApiResponseData.data.buildings.Last();
            UpdateResourcesAndTime();
            GameManager.instance.userPanelUI.SetUserData(ConstructionApiResponseData.data);
        }
    }
    public void PlaceBuildingFromBuildingData(Vector2Int pos, Building data)
    {
        if (instantiatedBuilding != null && grid.isCellValid(CurrentPos) == true)
        {
            var building = instantiatedBuilding.GetComponent<BuildingPrefab>();
            //instantiatedBuilding.GetComponent<BuildingPrefab>().buildingManager = GetComponent<BuildingManager>();
            // building.GetCurrentPos();
            building.Isplaced = true;
            var buildingdata = DataManager.instance.allBuildingsDataList.Find(i => i._id.Equals(data.buildingId));
            BuildingController entity = new BuildingController(SelectedBuildingId, pos, new Vector2Int(buildingdata.xSize, buildingdata.ySize));
            grid.CreatObject(entity, pos, new Vector2Int(buildingdata.xSize, buildingdata.ySize));
            building.CloseTickCrossPanel();
            print("Current pos" + pos);
        }

    }

    private void PlaceBuildingOnEmptyTile(Vector2Int obj)
    { // Click on New Building Image From building panel

        if (isConstructMode)
        {
            Vector2 pos = obj;
            InstantiateBuilding(SelectedBuildingId, pos);
        }
    }

    public void onBuildingCancel(Vector2Int pos, Vector2Int size)
    {
        // From Cancel from main menu user panel.
        var building = instantiatedBuilding.GetComponent<BuildingPrefab>();
        if (building.Isplaced == false)
        {
            print("DestroyingX" + pos.x + "DestroyingY" + pos.y);
            CanBeConstructed = true;
            Destroy(instantiatedBuilding);
            grid.RemoveObject(pos, size);
            isConstructMode = false;
        }
        buildingPrefabsList.Remove(building);

    }



    internal void PopulateBuildings(List<Building> buildings)
    {
        Debug.Log("Building Count is: " + buildings.Count);
        if (buildings.Count <= 0) return;
        foreach (var building in buildings)
        {
            //var data = GetBuildListData(building);
            Debug.Log("Building Cord from Api" + building.coordinates[0].x + building.coordinates[0].y);
            var category = DataManager.instance.GetAllBuildingsData().FirstOrDefault(i => i._id.Equals(building.buildingId)).category;
            Vector2 pos = new Vector2(building.coordinates[0].x, building.coordinates[0].y);
            InstantiateBuilding(building, pos);
            AddToPrefabsList(category, building);
            PlaceBuildingFromBuildingData(new Vector2Int(building.coordinates[0].x, building.coordinates[0].y), building);
        }
    }
    // used when building is instantiated from database
    void AddToPrefabsList(int category, Building data)
    {
        BuildingPrefab buildingprefabdata = instantiatedBuilding.gameObject.GetComponentInChildren<BuildingPrefab>();
        buildingprefabdata.category = category;
        buildingprefabdata.CurrentBuildingData = data;
        buildingPrefabsList.Add(buildingprefabdata);
        buildingprefabdata.data = DataManager.instance.allBuildingsDataList.Find(i => i._id.Equals(data.buildingId));
    }
    // used when new building is created
    void AddToPrefabsList()
    {
        BuildingPrefab buildingprefabdata = instantiatedBuilding.gameObject.GetComponentInChildren<BuildingPrefab>();
        buildingprefabdata.category = selectedBuildingData.category;
        buildingprefabdata.CurrentBuildingData = new Building();
        buildingPrefabsList.Add(buildingprefabdata);
        buildingprefabdata.data = selectedBuildingData;
    }
    public void CreatTileOnGrid(int Xpos, int yPos)
    {
        tile = Resources.Load<GameObject>("Prefabs/Tile");
        GameObject instantiatedTile = Instantiate(tile, gridParent.transform);
        Vector3 spawnPosition = TileMapper.GetWorldPosition(new Vector2(Xpos, yPos));
        instantiatedTile.transform.position = spawnPosition;
        instantiatedTile.name = Xpos + ", " + yPos;
        GridGameObjects.Add(instantiatedTile);
    }
    public void GenerateGridTiles()
    {
        for (int x = -32; x < 32; x++)
        {
            for (int y = -32; y < 32; y++)
            {
                GameObject instantiatedTile = Instantiate(tile, gridParent.transform);
                Vector3 spawnPosition = TileMapper.GetWorldPosition(new Vector2(x, y));
                //spawnPosition.z = -0.25f;
                instantiatedTile.transform.position = spawnPosition;
                instantiatedTile.name = x + ", " + y;
                //GridCell cell = new GridCell(x,y);
                //tilesDictionary.Add(new Vector2(x,y),cell) ;
            }
        }
    }

    internal void CreateNewBuildingPressed(BuildingsData data)
    {
        Debug.Log("Selected Building is " + data.image + "size is" + data.xSize);
        BuildingName = data.title;
        SelectedBuildingSizeX = data.xSize;
        SelectedBuildingSizeY = data.ySize;
        SelectedBuildingId = data._id;
        selectedBuildingData = data;
        isConstructMode = true;

        //if (CanBeConstracted == true)
        //{
        CanBeConstructed = false;
        var tile = grid.GetEmptyCellXY(new Vector2Int(data.xSize, data.ySize));
        PlaceBuildingOnEmptyTile(new Vector2Int(tile.x, tile.y));


        //}

    }
    ///instantiates new building 
    public void InstantiateBuilding(string id, Vector2 pos)
    {
        var path = DataManager.instance.GetAllBuildingsData()?.FirstOrDefault(i => i._id.Equals(id))?._id;
        GameObject BuildingPrefab = Resources.Load<GameObject>("Prefabs/Buildings/" + path);
        instantiatedBuilding = Instantiate(BuildingPrefab, gridParent.transform);
        //instantiatedBuilding.GetComponent<BuildingPrefab>().buildingManager = this;
        BuildingPrefab.name = id;
        Vector3 spawnPosition = TileMapper.GetWorldPosition(pos, true);
        instantiatedBuilding.transform.position = spawnPosition;
        AddToPrefabsList(); /// adding new building object when creating a new prefab
    }
    ///instantiates user buildings in database
    public void InstantiateBuilding(Building building, Vector2 pos)
    {
        GameObject BuildingPrefab = Resources.Load<GameObject>("Prefabs/Buildings/" + building.buildingId);
        instantiatedBuilding = Instantiate(BuildingPrefab, gridParent.transform);
        //instantiatedBuilding.GetComponent<BuildingPrefab>().buildingManager = this;
        BuildingPrefab.name = building.buildingId;
        Vector3 spawnPosition = TileMapper.GetWorldPosition(pos, true);
        instantiatedBuilding.transform.position = spawnPosition;
    }

    public GridCell GetCellInfo(Vector3 pos)
    {
        return tilesDictionary[TileMapper.GetTileCoordinatesFromPosition(pos)];
    }
    public void RefreshAllBuildingsStates(List<Building> buildings)
    {
        foreach (var building in buildings)
        {
            BuildingPrefab foundBuilding = buildingPrefabsList.Find(i => i.CurrentBuildingData._id.Equals(building._id));
            foundBuilding.updateBuildingState(building);
        }
    }

    public void InstantUpgradeBuildingState(string id, EnumBuildingStates EnumbuildingState)
    {

        switch (EnumbuildingState)
        {
            case EnumBuildingStates.upgrade:

                BuildingPrefab foundBuilding = buildingPrefabsList.Find(i => i.CurrentBuildingData._id.Equals(id));
                foundBuilding.RequiredTime = TimeManager.instance.serverTime;
                foundBuilding.RequiredTime = foundBuilding.RequiredTime.AddSeconds(foundBuilding.data.costPerLevel[foundBuilding.CurrentBuildingData.currentLevel].time);
                foundBuilding.updateBuildingState(EnumBuildingStates.upgrade);
                ApiController.UpdateBuildingAPI(UpdateBuildingCallBack, SignInWithTwitter.losAcessToken, SelectedBuildingUniqueId);
                break;
            case EnumBuildingStates.construct:

                BuildingPrefab CreatedfoundBuilding = buildingPrefabsList.Last();
                CreatedfoundBuilding.RequiredTime = TimeManager.instance.serverTime;
                CreatedfoundBuilding.RequiredTime = CreatedfoundBuilding.RequiredTime.AddSeconds(CreatedfoundBuilding.data.costPerLevel[0].time);
                CreatedfoundBuilding.updateBuildingState(EnumBuildingStates.construct);
                StartCoroutine(CallConstructApi());
                break;
            default:
                break;
        }

    }
    public IEnumerator CallConstructApi()
    {
        yield return new WaitForSeconds(0f);
        ApiController.ConstructBuildingAPI(ConstructCallBack, SignInWithTwitter.losAcessToken, SelectedBuildingId, CurrentPos.x, CurrentPos.y);
    }

    // SignInResponse updatedBuildingsResponseData = new SignInResponse();
    void UpdateBuildingCallBack(bool success, string response)
    {
        print(success);
        print(response);
        if (!success)
            return;
        else
        {
            // updatedBuildingsResponseData = JsonConvert.DeserializeObject<SignInResponse>(response);
            UpdateResourcesAndTime();
        }
    }




}
public enum EnumBuildingStates
{
    upgrade, construct,idle
}

