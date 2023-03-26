using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomePanelUI : MonoBehaviour
{
    public Button nextbtn;
    public Button playbtn;
    public Text nextBtnTxt;
    public Text description;
    public Text title;
    public string[] desTxt;
    public string[] titleTxt;
    int count = 0;

    private void Awake()
    {
        nextbtn.onClick.AddListener(Onclick);
        playbtn.onClick.AddListener(OnPlayClicked);
        SetOnStart();
    }
    private void SetOnStart()
    {
        nextBtnTxt.text = ("Next (" + (count + 1) + " of " + desTxt.Length + ")").ToString();
        description.text = desTxt[count];
        title.text = titleTxt[count];
        count++;
    }
    public void Onclick()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        nextBtnTxt.text = ("Next (" + (count+1) + " of " + desTxt.Length+")").ToString();
        description.text = desTxt[count];
        title.text = titleTxt[count];
        count++;
        if(count == desTxt.Length)
        {
            nextbtn.gameObject.SetActive(false);
            playbtn.gameObject.SetActive(true);
        }

    }
    public void OnPlayClicked()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        gameObject.SetActive(false);
    }
    public void SetText(string errorCode, string Description)
    {
        nextBtnTxt.text = "OK";
        // errorCodeText.text = errorCode;
        title.text = "Error "+ errorCode;

        description.text = Description;
        nextbtn.onClick.RemoveAllListeners();
        nextbtn.onClick.AddListener(CloseWindow);
    }
    void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
