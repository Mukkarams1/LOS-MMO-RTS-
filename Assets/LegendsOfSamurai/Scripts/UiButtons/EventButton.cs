using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventButton : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(onClickgiftBtn);
    }
    private void onClickgiftBtn()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        GameManager.instance.ShowAvailableGifts();
    }
}
