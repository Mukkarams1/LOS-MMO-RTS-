using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBtn : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenPanel);
    }
    public void OpenPanel()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        GameManager.instance.activeBoosterPnl();
    }
}
