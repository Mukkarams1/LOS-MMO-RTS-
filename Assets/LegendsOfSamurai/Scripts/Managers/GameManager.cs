using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject userGIftsPanel;
    [SerializeField]
    Transform giftsParent;

    public GameObject Mail;
    public GameObject BoosterPnl;
    public BuildingManager buildingManager;
    public GameObject TroopPnl;
    public GameObject BuildingPnl;
    public GameObject UpgradePnl;
    public GameObject SpeedUpPnl;
    public GameObject StrategyPnl;
    public GameObject ChatPnl;
    public GameObject TaskPnl;
    public GameObject WarehousePnl;
    public GameObject buildingViewPanel;
    [SerializeField]
    GameObject TroopViewPanel;
    public BuildingBtnUi buildingButtonsPanel;
    public static GameManager instance;
    public InputManager inputManager;
    public UserPanelUI userPanelUI;
    public userTroopUI userTroopUI;
    [SerializeField]
    NotificationPanelUI notificationPanelUI;
    GridSystem grid;
    public PlayerTasksManager playerTasksManager;
    List<Gift> userGiftsList = new List<Gift>();

    [SerializeField]
    GameObject animPrefab;
    [SerializeField]
    Transform Parent;
    [SerializeField] Transform startPoint;
    [SerializeField]
    Transform userpanelPos;

    [Space(10)] // SpaceInInspector
    [Header("On Game ReOpen")] // HeaderInInspector
    [Space(20)] // SpaceInInspector
    public GameObject reopenPanel;
    internal void InitializeGrid(List<Building> buildings)
    {
        if (buildings.Count <= 0) return;
        buildingManager.PopulateBuildings(buildings);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            OnGameReOpen();
            buildingManager.UpdateResourcesAndTime();
        }
    }

    private void OnGameReOpen()
    {
        reopenPanel.SetActive(true);
    }

    private void Awake()
    {
        // print(TileMapper.GetTileCoordinatesFromPosition(new Vector3(0, 7.5f, 0)));
        // ConfirmBtn.onClick.AddListener(OnConfirmClicked);
        buildingManager = FindObjectOfType<BuildingManager>();
        playerTasksManager = FindObjectOfType<PlayerTasksManager>();
        ApiController.onRefreshTokenGenerationFailed += MoveToMainScene; 
        ApiController.onRefreshTokenGenerationSuccessful += AccessTokenUpdated; 
        DontDestroyOnLoad();
        if (instance == null)
            instance = this;
     

    }

    internal void creatGiftRedeemAmin(Transform pos)
    {
        //for(int j = 1; j < 6; j++)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        var spread = pos.position + new Vector3(UnityEngine.Random.Range(-100 , 100), 0f, 0f);
        //        var prefab = Instantiate(animPrefab, spread, Quaternion.identity, Parent);
        //        prefab.GetComponent<giftAnimPrefab>().SetUI(j);
        //        MovePrefabToPosition(prefab);
        //    }
            
        //}
    }
    public void test()
    {

            for (int i = 0; i < 9; i++)
            {
                var spread = startPoint.transform.position + new Vector3(UnityEngine.Random.Range(-100, 100), 0f, 0f);
                var prefab = Instantiate(animPrefab, spread, Quaternion.identity, Parent);
                MovePrefabToPosition(prefab);
            }
  
    }

    private void MovePrefabToPosition(GameObject gameObject)
    {
        gameObject.transform.DOMove(userpanelPos.position, 1.5f)
            .SetEase(Ease.InOutBack)
            .OnComplete(() => {
            Destroy(gameObject);
        });

    }
    private void OnDisable()
    {
        ApiController.onRefreshTokenGenerationFailed -= MoveToMainScene;
        ApiController.onRefreshTokenGenerationSuccessful -= AccessTokenUpdated;

    }
    void AccessTokenUpdated()
    {
        PlayerPrefs.SetString("losAcessToken", SignInWithTwitter.losAcessToken);
        PlayerPrefs.SetString("losRefreshToken", SignInWithTwitter.losRefreshToken);
        buildingManager.UpdateResourcesAndTime();
    }
    public void MoveToMainScene(string errorCode, String Msg)
    {
        
        ShowNotificationPopUpUI(errorCode + " " + Msg+" " + "Loading Main Scene , SignInAgain");
        SceneManager.LoadSceneAsync(0);
    }
    //Add method of Confirming and placing the building on the gird.
      //Creat object on Grid.
      //Creat world entity.
      //Set gird to not Constrauctable.
    public void OnConfirmClicked()
    {
        //BuildingManager.instance.PlaceObjectAfterDrag();
        
    }
    private void Start()
    {

        DataManager.onUserDataUpdated += UserDataUpdated;
        ShowAvailableGifts();
        // InitializePlayer();
        //grid = new GridSystem(64,64);
    }
    public void UserDataUpdated()
    {
        ShowAvailableGifts();
    }
    public void ShowAvailableGifts()
    {
        buildingButtonsPanel.gameObject.SetActive(false);
        userGiftsList = DataManager.instance.GetUserGiftsData();    
        if (userGiftsList==null || userGiftsList.Count == 0 || giftsParent.childCount>0)
            return;
        else 
        {
            foreach (var gift in userGiftsList)
            {
                GameObject giftObject = Instantiate(userGIftsPanel, giftsParent);
                giftObject.GetComponent<UserGiftPrefab>().SetUI(gift);

            }
        }
    }

    private void InitializePlayer()
    {
       
    }
    public void ShowNotificationPopUpUI(string notification)
    {
       if (TaskPnl.activeInHierarchy)
            TaskPnl.SetActive(false);

        buildingButtonsPanel.gameObject.SetActive(false);
        notificationPanelUI.GetComponent<NotificationPanelUI>().SetNotificationText(notification);
        notificationPanelUI.gameObject.SetActive(true);

    }
    public void ToggleWareHousePanel(bool value)
    {
        buildingButtonsPanel.gameObject.SetActive(false);

        WarehousePnl.SetActive(value);
    }
    public void activeBoosterPnl()
     {
        buildingButtonsPanel.gameObject.SetActive(false);

        BoosterPnl.SetActive(true);
     }
    public void activeTroopPnl(bool toggleMenu)
    {
        buildingButtonsPanel.gameObject.SetActive(false);
        TroopPnl.SetActive(toggleMenu);
    }
    public void activeBuildingPnl()
    {
        buildingButtonsPanel.gameObject.SetActive(false);

        BuildingPnl.SetActive(true);
    }
    public void activeUpgradePnl()
    {
        buildingButtonsPanel.gameObject.SetActive(false);
        UpgradePnl.SetActive(true);
    }
    public void activeBuildingViewPnl()
    {
        buildingButtonsPanel.gameObject.SetActive(false);
        buildingViewPanel.SetActive(true);
    }
    public void activeSpeedPnl()
    {
        buildingButtonsPanel.gameObject.SetActive(false);
        SpeedUpPnl.SetActive(true);
    }
    public void activeStrategyPnl()
    {
        buildingButtonsPanel.gameObject.SetActive(false);

        StrategyPnl.SetActive(true);
    }
    public void setChatPanelActive()
    {
        buildingButtonsPanel.gameObject.SetActive(false);

        ChatPnl.SetActive(true);
    }

    public void ToggleBuildingButtonsPanel(bool toggle)
    {
        buildingButtonsPanel.gameObject.SetActive(toggle);
    }
    public void ToggleTroopPanel()
    {
        buildingButtonsPanel.gameObject.SetActive(false);
        TroopViewPanel.SetActive(!TroopViewPanel.activeInHierarchy);
    }
    private void Update()
    {
       if (!ApiController.IsConnectedToInternet())
        {
            ShowNotificationPopUpUI("internet disconnected");
        }
    }
    void DontDestroyOnLoad()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
