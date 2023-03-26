using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingPrefab : MonoBehaviour, IIntractable
{
    Button recruitTroopButton;
    public Button ConfirmBtn;
    public Button cancelBtn;

    [SerializeField]
    Timer timer;


    private bool isDragging;
    public bool Isplaced;
    public GameObject TickCrossPanel;
    public SpriteRenderer Background;
    public SpriteRenderer building;
    private Sprite DefaultImage;
    public float tileSize;
    public int category;
    public Vector2Int tilePosition;
    public Text BuildingLVlText;
    public GameObject BuildingLVlPanel;
    public GameObject BuildingBoosterIcon;
    TimeSpan deltaTimeSpan;
    TimeSpan ZeroTime;

    public DateTime RequiredTime;
    TouchPhase touchPhase = TouchPhase.Ended;

    public Building CurrentBuildingData;
    public BuildingsData data;
    BoxCollider2D boxCollider2D = new BoxCollider2D();

    public  EnumBuildingStates currentBuildingStatus;
    private void Awake()
    {
        DefaultImage = building.sprite;
    }
    private void Start()
    {
        boxCollider2D = building.gameObject.GetComponent<BoxCollider2D>();
        DataManager.onUserDataUpdated += UpdateStates;
        Debug.Log("My Name is " + data.title);
        BuildingLVlPanel.SetActive(false);
        ZeroTime = new TimeSpan(00, 00, 00);
        // timer = timerText.transform.parent.gameObject;                  //todo timer correction
        timer.gameObject.SetActive(false);
        ConfirmBtn.onClick.AddListener(OnConfirmClicked);
        cancelBtn.onClick.AddListener(OnCancelClicked);
        GetCurrentPos();
        RequiredTime = CurrentBuildingData.requiredTime;
        category = data.category;
        if (category==2)
        {
            recruitTroopButton = this.transform.GetComponentsInChildren<Button>().FirstOrDefault(i=>i.name.Equals("RecruitButton"));
            recruitTroopButton.transform.parent = timer.transform;
            recruitTroopButton.gameObject.SetActive(false);                   // todo recruitbtn 
            recruitTroopButton.onClick.AddListener(RecruitTroopButtonPressed);  // todo recruitbtn setting
        }
        updateBuildingState(EnumBuildingStates.upgrade);
        ShowBoosterIcon();
    }
    private void OnDisable()
    {
        DataManager.onUserDataUpdated -= UpdateStates;

    }
    public void OnCancelClicked()
    {
        Cancel();
    }
    public void OnConfirmClicked()
    {
        GameManager.instance.buildingManager.onConfirmClicked(data);
    }

    public void CloseTickCrossPanel()
    {
        TickCrossPanel.SetActive(false);
    }
    public void SetBuildingLvl(int BuildingLvl)
    {
        BuildingLVlText.text = BuildingLvl.ToString();
      //  SetBuildingSprite();

        if ((BuildingLvl <= 0) || Isplaced == false)
        {
            BuildingLVlPanel.SetActive(false);

        }
        else
        {
            BuildingLVlPanel.SetActive(false); //// todo temporarily hidden
        }
    }
    void Update()
    {
        // IsTouched();
        //TouchEnded();

        GetCurrentPos();
        if (data != null)
          //  SetBackGroundColor(IsConstructable());
        if (Isplaced == false)
        {
            Drag();
            DestroyPrefabOnUiClick();
        }

    }
    public void SetBuildingSprite()
    {
        // is placed bit is true when building's construction is completed
        Debug.Log("CurrentBuildingData.currentLevel " + CurrentBuildingData.currentLevel);
        //building.sprite = CurrentBuildingData.currentLevel>0 ? DefaultImage : Resources.Load<Sprite>("BuildingImage/UnderConstructionSprite");
    }

    private void DestroyPrefabOnUiClick()
    {
        if (Input.GetMouseButtonDown(0) &&
            GameManager.instance.inputManager.IsPointerOverUIObject() &&
            !GameManager.instance.inputManager.ObjUnderPointerName().Equals("Cross") &&
            !GameManager.instance.inputManager.ObjUnderPointerName().Equals("Tick"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //OnCancelClicked();
            }
        }
    }

    public IEnumerator WaitCallResourcesApi()
    {
        yield return new WaitForSeconds(3);
        CallUpdateTimeAndResourceAPI();
    }

    void UpdateStates()
    {
       // ShowBoosterIcon();
        Building foundBuilding = DataManager.instance.GetUserBuildingsData().Where(i => i._id.Equals(CurrentBuildingData._id))?.FirstOrDefault();
        if (foundBuilding != null)
            InstantupdateBuildingState(foundBuilding);
    }
    void ShowBoosterIcon()
    {
        //Booster booster = DataManager.instance.GetUserAppliedBoosters().Where(i => i.inventoryType.Equals(data.yieldCategory))?.FirstOrDefault();
        //// if booster for this catergory exists then show Booster Icon on Building
        //    BuildingBoosterIcon.gameObject.SetActive(booster != null && booster.expiry > TimeManager.instance.serverTime);
    }
    public void updateBuildingState(Building building)
    {
        CurrentBuildingData = building;
        RequiredTime = CurrentBuildingData.requiredTime;
        SetBuildingLvl(building.currentLevel);
        SetBuildingSprite();
        if (RequiredTime > TimeManager.instance.serverTime && !timer.timerCoroutineRunning)
        {
            double seconds = RequiredTime.Subtract(TimeManager.instance.serverTime).TotalSeconds;
            timer.gameObject.SetActive(true);
            timer.StartTimerCountdown((int)seconds,CurrentBuildingData._id);
        }
    }
    public void InstantupdateBuildingState(Building building)
    {
        CurrentBuildingData = building;
        RequiredTime = CurrentBuildingData.requiredTime;
        SetBuildingLvl(building.currentLevel);
        SetBuildingSprite();
    }
    public void SetBackGroundColor(bool isConstructable)
    {
        if (isConstructable == false)
        {
            Background.color = Color.red;
            ConfirmBtn.interactable = false;
        }
        else
        {
            Background.color = Color.green;
            ConfirmBtn.interactable = true;
        }
        if (Isplaced == true)
        {
            Background.gameObject.SetActive(false);
        }
    }
    public bool IsConstructable()
    {
        return GameManager.instance.buildingManager.IsConstructable(TileMapper.GetTileCoordinatesFromPosition(gameObject.transform.position), new Vector2Int(5, 5));
    }
    public void GetCurrentPos()
    {

        GameManager.instance.buildingManager.CurrentPos = TileMapper.GetTileCoordinatesFromPosition(gameObject.transform.position);
        tilePosition = TileMapper.GetTileCoordinatesFromPosition(gameObject.transform.position);
    }
    public void OnMouseDown()
    {
        isDragging = true;

        // Debug.Log("id setted to " + CurrentBuildingData.buildingId);
    }
    public void IsTouched()
    {
        //TouchPhase touchPhase = TouchPhase.b
        if (Input.touchCount == 1) //&& Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.name == gameObject.name)
                {
                    Debug.Log(hit.collider.name + "Is Touched");
                    isDragging = true;
                }
                else
                {
                    isDragging = false;
                }
            }
        }
    }
    public void TouchEnded()
    {
        if (Input.GetTouch(0).phase == touchPhase)
        {

        }
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    /// <summary>
    /// drag variables
    /// </summary>
    float tx;
    float ty;
    private void Drag()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            tx = Mathf.FloorToInt(mousePosition.x / 0.5f) * 0.5f;
            ty = Mathf.FloorToInt(mousePosition.y / 0.5f) * 0.5f;

            if (tx == 0.5 || tx == -0.5)
            {
                ty = 0.25f;
                transform.Translate(new Vector2(tx, ty));
            }
            else
            {
                transform.Translate(new Vector2(tx, ty));
            }
            if (!Isplaced)
            {
                var mousePos = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                var pos = TileMapper.GetTileCoordinatesFromPosition(mousePos);
                transform.position = TileMapper.GetWorldPosition(pos, true);
            }
        }
    }
    public void Interacted()
    {
        if (Isplaced == true)
        {
            GameManager.instance.buildingManager.onBuildingIntracted(CurrentBuildingData, this, category);
            print("X:" + tilePosition.x + " Y:" + tilePosition.y);
        }
    }

    public void Cancel()
    {
        GameManager.instance.inputManager.Cancel(tilePosition, new Vector2Int(2, 2));
    }

    public void updateBuildingState(EnumBuildingStates buildingState)
    {
        if (Isplaced == true)
        {
            switch (buildingState)
            {
                case EnumBuildingStates.upgrade:

                    if (RequiredTime > TimeManager.instance.serverTime && !timer.timerCoroutineRunning)
                    {
                        double seconds = RequiredTime.Subtract(TimeManager.instance.serverTime).TotalSeconds;
                        timer.gameObject.SetActive(true);
                        timer.StartTimerCountdown((int)seconds, CurrentBuildingData._id);
                        SetBuildingLvl(CurrentBuildingData.currentLevel);
                        SetBuildingSprite();
                        currentBuildingStatus = EnumBuildingStates.upgrade;

                    }
                    else
                    {
                    currentBuildingStatus = EnumBuildingStates.idle;   // on start upgrade is checked, to start timer in case req time is
                                                                       // greater, if not greater then set building to idle state
                    }
                    break;

                case EnumBuildingStates.construct:
                    if (RequiredTime > TimeManager.instance.serverTime && !timer.timerCoroutineRunning)
                    {
                        double seconds = RequiredTime.Subtract(TimeManager.instance.serverTime).TotalSeconds;
                        timer.gameObject.SetActive(true);
                        timer.StartTimerCountdown((int)seconds, CurrentBuildingData._id);
                        SetBuildingLvl(0);
                        SetBuildingSprite();
                        currentBuildingStatus = EnumBuildingStates.construct;

                    }
                    break;
                case EnumBuildingStates.idle:
                    currentBuildingStatus = EnumBuildingStates.idle;
                    building.sprite = DefaultImage ;

                    break;
                default:
                    break;
            }

        }

    }
    public void StartTroopTimerCountdown(int seconds)
    {
        timer.gameObject.SetActive(true);
        timer.StartTimerCountdown((int)seconds, CurrentBuildingData._id ,true);
    }

    void RecruitTroopButtonPressed()
    {
        CallUpdateTimeAndResourceAPI();
        //ToggleCollider(true);
        recruitTroopButton.gameObject.SetActive(false); // todo recruitbtn setting
        timer.gameObject.SetActive(false);
    }
    public void ActivateRecruitBtn()
    {
        recruitTroopButton.gameObject.SetActive(true);
    }
    void CallUpdateTimeAndResourceAPI()
    {
        ApiController.ResourceAndTimeUpdateAPI(DataManager.instance.UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken);
    }
    public Timer GetTimer()
    {
        return timer;
    }
}