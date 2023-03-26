using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailButton : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(OnclickMailBtn);
    }

    private void OnclickMailBtn()
    {
        SoundManager.instance.PlaySound("ButtonClick");
    }


}
