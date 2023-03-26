using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyPanelUI : MonoBehaviour
{
    public Button Clocsebtn;
    void Start()
    {
        Clocsebtn.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }
}
