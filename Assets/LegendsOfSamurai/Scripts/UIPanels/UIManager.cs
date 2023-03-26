using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button BuildButton;
    public GameObject BuildPanel;
    void Start()
    {
        BuildButton.onClick.AddListener(OnBuildButtonClicked);
    }

    private void OnBuildButtonClicked()
    {
        BuildPanel.gameObject.SetActive(true);
    }
}
