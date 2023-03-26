using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour
{
    public Text InputText;
    public Button LoginBtn;
    public GameObject WrongEmailUI;
    public GameObject EnterEmailTxt;
    
    // Fix Wrong Email Function.
    void Start()
    {
        LoginBtn.GetComponent<Button>().onClick.AddListener(OnLoginBtnClicked);
    }

    private void OnLoginBtnClicked()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        if(InputText.text  == "")
        {
            EnterEmailTxt.SetActive(true);
            Invoke("DisableEmailUI", 2);
            return;
        }
        else
        {
            //EnterEmailTxt.SetActive(false);
            //for (int i = 0; i < DataManager.instance.userData.Count; i++)
            //{
            //    string dataemail = DataManager.instance.userData[i].email;
            //    if (InputText.text == dataemail)
            //    {
            //        SetUserDataAndChangeScene();
            //        return;
            //    }
            //    else if ( i == DataManager.instance.userData.Count -1)
            //    {
            //        WrongEmailUI.SetActive(true);
            //        Invoke("DisableEmailUI", 2);
                    
            //    }
            //}
        }
        
    }
    void DisableEmailUI()
    {
        WrongEmailUI.SetActive(false);
        EnterEmailTxt.SetActive(false);
    }

    public void SetUserDataAndChangeScene()
    {
        Debug.Log("its runnig 2");
        PlayerPrefs.SetString("UserEmail", InputText.text);
        SceneManager.LoadScene(1);
    }
  
}
