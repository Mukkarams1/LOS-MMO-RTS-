using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPrefab : MonoBehaviour
{
    ChapterTask chapterTask;
    string chapterID;
    TaskPanelUi TaskPanelUi;
    public Text taskTitle;
    public Text taskWood;
    public Text taskFood;
    public Text taskMetal;
    public Text taskGunPowder;
    public Text taskGold;
    public Text taskDiamond;
    public Button ClaimBtn;
    // Start is called before the first frame update
    public static Action onClaimButtonPressed;
    private void Start()
    {
        ClaimBtn.onClick.AddListener(OnClaimClicked);
    }

    public void OnClaimClicked()
    {
        ApiController.AddCompletedTaskAPI(DataManager.instance.UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken, chapterID, chapterTask._id);
        ClaimBtn.interactable = false;
        ClaimBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Claimed";
        GameManager.instance.playerTasksManager.CheckUserCompletedTheChapter();
        SoundManager.instance.PlaySound("ButtonClick");
        GameManager.instance.creatGiftRedeemAmin(ClaimBtn.transform);
    }

    public void SetTaskData(string chapID, ChapterTask data, TaskPanelUi taskPanelUi)
    {
        chapterID = chapID;
        chapterTask = data;
        TaskPanelUi = taskPanelUi;
        UpdateUI();
    }

    private void UpdateUI()
    {
        taskTitle.text = chapterTask.taskTitle;
        taskFood.text = chapterTask.taskGifts[0].food.ToString();
        taskWood.text = chapterTask.taskGifts[0].wood.ToString();
        taskMetal.text = chapterTask.taskGifts[0].metal.ToString();
        taskGunPowder.text = chapterTask.taskGifts[0].gunPowder.ToString();
        taskGold.text = chapterTask.taskGifts[0].gold.ToString();
        taskDiamond.text = chapterTask.taskGifts[0].diamond.ToString();

        ClaimBtn.gameObject.SetActive(CheckTaskInventoryCriteria(chapterTask.criteriaNew.type));
        if (CheckTaskInventoryClaimed()) //if not claimed yet, then claim button is intractable otherwise not.
        {
            ClaimBtn.interactable = !CheckTaskInventoryClaimed();
            ClaimBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Claimed";
        }
                }
    private bool CheckTaskInventoryClaimed()
    {
        if (DataManager.instance.GetUserChapterProgressData().Where(i => i._id.Equals(chapterID)).FirstOrDefault()?.tasks.Where(i => i.taskId.Equals(chapterTask._id)).FirstOrDefault() != null)
            return true;
        else
            return false;
    }
    private bool CheckTaskInventoryCriteria(string criteriaType)
    {
        MainInventory userMainInventoryData = DataManager.instance.GetUserMainInventoryData();
        if (Enum.TryParse<TaskCriteriaItemsEnum>(criteriaType, true, out TaskCriteriaItemsEnum result))
        {
            print(result);
            switch (result)
            {
                case TaskCriteriaItemsEnum.food:
                    if (userMainInventoryData.food >= chapterTask.criteriaNew.quantity )
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.wood:
                    if (userMainInventoryData.wood >= chapterTask.criteriaNew.quantity)
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.metal:
                    if (userMainInventoryData.metal >= chapterTask.criteriaNew.quantity)
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.gunPowder:
                    if (userMainInventoryData.gunPowder >= chapterTask.criteriaNew.quantity)
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.diamond:
                    if (userMainInventoryData.diamond >= chapterTask.criteriaNew.quantity)
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.gold:
                    if (userMainInventoryData.gold >= chapterTask.criteriaNew.quantity)
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.building:
                    List<Building> userBuildingsList = DataManager.instance.GetUserBuildingsData();
                    if (userBuildingsList.Where(i => i.buildingId.Equals(chapterTask.criteriaNew.itemID) && i.currentLevel >= chapterTask.criteriaNew.level).FirstOrDefault() != null)
                    {
                        return true;
                    }
                    break;
                case TaskCriteriaItemsEnum.troop:
                    List<Troop> userTroopsList = DataManager.instance.GetUserTroopsData();

                    if (userTroopsList.Where(i => i.troopId.Equals(chapterTask.criteriaNew.itemID) && i.quantity >= chapterTask.criteriaNew.quantity).FirstOrDefault() != null)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
        }

        return false;
    }
}
