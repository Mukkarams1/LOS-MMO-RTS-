using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public DateTime serverTime { get;private set; }

    private void Awake()
    {
        DontDestroyOnLoad();
        if (instance == null)
            instance = this;
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
    // Update is called once per frame
    void Update()
    {
        serverTime = serverTime.AddSeconds(Time.deltaTime);
        //Debug.Log("time manager server time" + serverTime);
        //Debug.Log("time manager delta time" + Time.deltaTime);
        //Debug.Log("time manager server time + delta time " + serverTime.AddSeconds(Time.deltaTime));
    }
    public void SetTime(DateTime time) { serverTime = time; }
}
