using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingViewPanelEntry : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI counterText;
    [SerializeField]
    TextMeshProUGUI LevelText;
    [SerializeField]
    TextMeshProUGUI Benefit;
    public void setUI(string counter, string level, string benefit)
    {
        counterText.text = counter;
        LevelText.text = level;
        Benefit.text = benefit;
    }
}
