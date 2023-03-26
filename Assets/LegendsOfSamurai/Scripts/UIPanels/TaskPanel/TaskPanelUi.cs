using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelUi : MonoBehaviour
{
    public GameObject TaskPrefab;
    public Transform TaskPlaceHolder;
    public Text TitleTxt;
    public Button closeBtn;

    List<ChapterData> chapterData;
    int userCurrentChapter;
    void Start()
    {
        closeBtn.onClick.AddListener(ClosePanel);

    }
    private void OnEnable()
    {
      //  DataManager.onUserDataUpdated += RefreshUI;
        PopulateUI();

    }

    void RefreshUI()
    {
        ClearScrollPanel();
        PopulateUI();
    }
    private void PopulateUI()
    {
        chapterData = DataManager.instance.GetAllChaptersData();
        userCurrentChapter =  DataManager.instance.SignInResponseData.data.currentChapterProgress;
        if (userCurrentChapter>=chapterData.Count)
        {
            GameManager.instance.ShowNotificationPopUpUI("All Chapters Completed");
            return;
        }


        /// level calcultation needs to be done in future just showing one value atm
        for (int i = 0; i < chapterData[userCurrentChapter].tasks.Count; i++)
        {
            TaskPrefab subjectobject = Instantiate(TaskPrefab, TaskPlaceHolder).GetComponent<TaskPrefab>();
            subjectobject.SetTaskData(chapterData[userCurrentChapter]._id, chapterData[userCurrentChapter].tasks[i], this);
        }
        SetTaskBarTitle();
    }
    private void ClosePanel()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        gameObject.SetActive(false);

    }

    void SetTaskBarTitle()
    {

        TitleTxt.text = chapterData[userCurrentChapter].chapterTitle + " > " + chapterData[userCurrentChapter].chapterDescription;
    }

    private void OnDisable()
    {
     //   DataManager.onUserDataUpdated -= RefreshUI;
        ClearScrollPanel();
    }
    private void ClearScrollPanel()
    {
        foreach (Transform child in TaskPlaceHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
