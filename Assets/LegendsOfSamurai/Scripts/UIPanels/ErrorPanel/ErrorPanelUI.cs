using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanelUI : MonoBehaviour
{
   
    [SerializeField]
    Text errorCodeText;
    [SerializeField]
    Text DescriptionText;
    [SerializeField]
    Button closeBtn;

    public void SetText(string errorCode, string Description)
    {
        closeBtn.GetComponentInChildren<Text>().text = "OK";
        gameObject.GetComponent<WelcomePanelUI>().enabled = false;
        errorCodeText.text = errorCode;
        DescriptionText.text = Description; 
        closeBtn.onClick.AddListener(CloseWindow);
    }
   void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}
