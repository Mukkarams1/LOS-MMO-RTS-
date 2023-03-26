using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningsBtn : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(OnClickWareHouseBtn);
    }

    private void OnClickWareHouseBtn()
    {
        SoundManager.instance.PlaySound("ButtonClick");
    }


}
