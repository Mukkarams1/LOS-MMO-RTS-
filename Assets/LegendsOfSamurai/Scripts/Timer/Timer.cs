using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour
{

    public Text timerText;
    public bool timerCoroutineRunning;
    string buildingID;
    [SerializeField]
    Slider Slider;
    DateTime reqTime = new DateTime();
    public void StartTimerCountdown(int seconds, string timerBuildingID ,bool isTroopTimer=false )
    {
        buildingID = timerBuildingID;
        Debug.Log("going to start timer");
        StartCoroutine(TimerCountdownCoroutine(seconds,isTroopTimer));
        Slider.maxValue = seconds;
        Slider.DOValue(Slider.maxValue, seconds);
    }
    private IEnumerator TimerCountdownCoroutine(int seconds, bool isTroopTimer=false)
    {
        reqTime = TimeManager.instance.serverTime;
        print("datetime before adding seconds " + reqTime);
        reqTime = reqTime.AddSeconds(seconds+0.99f);
        print("datetime after adding seconds " + reqTime);
        ShowTime(reqTime.Subtract(TimeManager.instance.serverTime));
        timerCoroutineRunning = true;
        while (reqTime.Subtract(TimeManager.instance.serverTime) > TimeSpan.Zero)
        {
            //Debug.Log("reqire time is great");
            yield return new WaitForSeconds(1);
            
            ShowTime(reqTime.Subtract(TimeManager.instance.serverTime));
        }
        timerCoroutineRunning = false;
        //StartCoroutine(DelayedTask(5));
        GameManager.instance.buildingManager.UpdateResourcesAndTimeWithDelay();
        UpdateBuildingState();
        if (isTroopTimer && (buildingID!=null || buildingID != string.Empty))
        {
            TroopTimerEnded();
        }
        else
        {
        this.gameObject.SetActive(false);
        }
    }
    void ShowTime(TimeSpan remainingTime)
    {
        timerText.text = remainingTime.ToString(@"hh\:mm\:ss");
    }
    public void UpdateTimerTextOnly(string time)
    {
        timerText.text = time;
    }

    void TroopTimerEnded()
    {
       GameManager.instance.buildingManager.buildingPrefabsList.FirstOrDefault(i => i.CurrentBuildingData._id.Equals(buildingID))?.ActivateRecruitBtn();
    }
    private void UpdateBuildingState()
    {
       GameManager.instance.buildingManager.buildingPrefabsList.FirstOrDefault(i => i.CurrentBuildingData._id.Equals(buildingID))?.updateBuildingState(EnumBuildingStates.idle);

    }
    public void AddSpeedUpItemEffect(int seconds)
    {

        DateTime tempTime =  reqTime;
        tempTime = tempTime.AddSeconds(-seconds);
        Slider.maxValue -= seconds;
        reqTime = tempTime;
    }
    private void OnDisable()
    {
        if (Slider!=null)
        {
        Slider.value = 0;
        }
    }
}
