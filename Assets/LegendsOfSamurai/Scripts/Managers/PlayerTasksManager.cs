using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTasksManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tasksCounterText;
    [SerializeField]
    GameObject tasksCounterObj;
    [SerializeField]
    TextMeshProUGUI mailsCounterText;
    [SerializeField]
    GameObject mailsCounterObj;
    int currentChapter = 0;
    int userCompletedTasks = 0;
    List<ChapterData> chapterData = new List<ChapterData>();
    int currentChapterTasks = 0;
    private void OnEnable()
    {
        DataManager.onUserDataUpdated += UpdateData;
    }
    private void OnDisable()
    {
        DataManager.onUserDataUpdated -= UpdateData;

    }
    void UpdateData()
    {
        currentChapter = DataManager.instance.SignInResponseData.data.currentChapterProgress;
     
        SetUserCompletedTasks();
        SetTaskCounter();
        SetMailsCounter();
    }
    private void Start()
    {
        chapterData = DataManager.instance.GetAllChaptersData();
        currentChapter = DataManager.instance.SignInResponseData.data.currentChapterProgress;
        currentChapterTasks = currentChapter>=chapterData.Count? 0 : chapterData[currentChapter].tasks.Count ;

        SetUserCompletedTasks();


        SetTaskCounter();
        SetMailsCounter();
    }
    void SetUserCompletedTasks() {
        userCompletedTasks = DataManager.instance.SignInResponseData.data?.chapterProgress?.Count > 0 ?
               DataManager.instance.SignInResponseData.data.chapterProgress.Last().tasks.Count : 0;

    }
    public void CheckUserCompletedTheChapter()
    {
        //     bool isClaimed;
        //    foreach (var chapterTask in chapterData[currentChapter].tasks)
        ////   {
        //      isClaimed = DataManager.instance.SignInResponseData.data.chapterProgress.Where(i => i._id.Equals(chapterData[currentChapter]._id)).FirstOrDefault()?.
        //                       tasks.Where(j => j.taskId.Equals(chapterTask._id)).FirstOrDefault() != null;
        //   if (isClaimed)
        //     {
        userCompletedTasks++;
        //    }
        //}
        if (currentChapterTasks == userCompletedTasks)
        {
            //fire event
            currentChapter++;
            currentChapterTasks = currentChapter >= chapterData.Count ? 0 : chapterData[currentChapter].tasks.Count;
            userCompletedTasks = 0;
            print("fire chapter completion event");
            GameManager.instance.ShowNotificationPopUpUI("Congratulations!!! Chapter Completed");
        }
    }
    private void SetTaskCounter()
    {
        // when current chapter counter is greater than all the chapters count then no next chapter remaining hence no tasks remaining
        if (currentChapter >= chapterData.Count )
        {
            tasksCounterText.text = "0";
            tasksCounterObj.SetActive(false);
            return;
        }
        var taskCounter = GetClaimableTasksCount();
        
            tasksCounterText.text = taskCounter.ToString();
            tasksCounterObj.SetActive(taskCounter > 0);
        
    }
    public void SetMailsCounter()
    {
        int mailsCounter = DataManager.instance.GetUserEmailsData().Where(i=> !i.isRead ).ToList().Count;
        if (mailsCounter > 0)
        {
            mailsCounterObj.SetActive(mailsCounter > 0);
            mailsCounterText.text = mailsCounter.ToString();
        }
        else
        {
            mailsCounterText.text = "0";
            mailsCounterObj.SetActive(mailsCounter > 0);
        }
    }
     int GetClaimableTasksCount()
    {
        int claimableTasksCounter = 0;
        bool isClaimed;
        currentChapter = DataManager.instance.SignInResponseData.data.currentChapterProgress;
        MainInventory userMainInventoryData = DataManager.instance.GetUserMainInventoryData();
        foreach (var chapterTask in chapterData[currentChapter].tasks)
        {
            print(currentChapter);
            print(chapterData[currentChapter]._id);
            isClaimed = DataManager.instance.SignInResponseData.data?.chapterProgress?.FirstOrDefault(i => i._id.Equals(chapterData[currentChapter]._id))?.
                 tasks.FirstOrDefault(j => j.taskId.Equals(chapterTask._id)) != null;  // if obj found in my list then it is claimed

            if (!isClaimed && Enum.TryParse<TaskCriteriaItemsEnum>(chapterTask.criteriaNew.type, true, out TaskCriteriaItemsEnum result))
            {
                switch (result)
                {
                    case TaskCriteriaItemsEnum.food:
                        if (userMainInventoryData.food >= chapterTask.criteriaNew.quantity)
                        {
                            claimableTasksCounter++;
                        }
                        break;
                    case TaskCriteriaItemsEnum.wood:
                        if (userMainInventoryData.wood >= chapterTask.criteriaNew.quantity)
                        {
                            claimableTasksCounter++;
                        }
                        break;
                    case TaskCriteriaItemsEnum.metal:
                        if (userMainInventoryData.metal >= chapterTask.criteriaNew.quantity)
                        {
                            claimableTasksCounter++;
                        }
                        break;
                    case TaskCriteriaItemsEnum.gunPowder:
                        if (userMainInventoryData.gunPowder >= chapterTask.criteriaNew.quantity)
                        {
                            claimableTasksCounter++;
                        }
                        break;
                    case TaskCriteriaItemsEnum.diamond:
                        if (userMainInventoryData.diamond >= chapterTask.criteriaNew.quantity)
                        {
                            claimableTasksCounter++;

                        }
                        break;
                    case TaskCriteriaItemsEnum.gold:
                        if (userMainInventoryData.gold >= chapterTask.criteriaNew.quantity)
                        {
                            claimableTasksCounter++;
                        }
                        break;
                    case TaskCriteriaItemsEnum.building:
                        List<Building> userBuildingsList = DataManager.instance.GetUserBuildingsData();
                        if (userBuildingsList.Where(i => i.buildingId.Equals(chapterTask.criteriaNew.itemID) && i.currentLevel >= chapterTask.criteriaNew.level).FirstOrDefault() != null)
                        {
                            claimableTasksCounter++;
                        }
                        break;
                    case TaskCriteriaItemsEnum.troop:
                        List<Troop> userTroopsList = DataManager.instance.GetUserTroopsData();
                        
                        
                            if (userTroopsList.Where(i => i.troopId.Equals(chapterTask.criteriaNew.itemID) && i.quantity >= chapterTask.criteriaNew.quantity).FirstOrDefault() != null)
                            {
                                claimableTasksCounter++;
                            }
                        
                        
                        break;
                    default:
                        break;
                }
            }
        }
        return claimableTasksCounter;
    }
}
