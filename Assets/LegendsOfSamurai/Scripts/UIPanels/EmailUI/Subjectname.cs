using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Subjectname : MonoBehaviour
{
    public Text subjecttxt;
    Mail emailData;
    EmailPanelUi panelUI;
    public Image emailImg;
    public Sprite readEmailImg;
    private Button Btn;
    public Image btnImage;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSubjectClicked);
    }

    private void OnSubjectClicked()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        MarkAsRead();
        panelUI.UpdatePanelDataInUI(emailData);
    }

    public void SetEmailData(Mail data, EmailPanelUi emailPanelUi)
    {
        emailData = data;
        panelUI = emailPanelUi;
        UpdateUI();
    }

    public void UpdateUI()
    {
        subjecttxt.text = emailData.subject;
        if (emailData.isRead)
        {
            emailImg.sprite = readEmailImg;
            btnImage = gameObject.GetComponent<Image>();
            btnImage.color = new Color(btnImage.color.r, btnImage.color.g, btnImage.color.b, 0.2f);
        }       
    }



    void MarkAsRead()
    {
        if (!emailData.isRead)
        {
        ApiController.UpdateMailStatusAPI(EmailStatusUpdatedCallBack, SignInWithTwitter.losAcessToken, emailData._id);
        UpdateMailStatus(true);
        }
    }
    void EmailStatusUpdatedCallBack(bool status, string resp)
    {
        if (!status)
        {
            print(resp);
            // could not update mail.
            UpdateMailStatus(false);
            return;
        }
        EmailUpdateResponse emailUpdateResponse = JsonConvert.DeserializeObject<EmailUpdateResponse>(resp);
        var Mail = DataManager.instance.SignInResponseData.data.mails.Find(i => i._id.Equals(emailData._id));
        Mail.isRead = true;
        GameManager.instance.playerTasksManager.SetMailsCounter();
        // in case if needs to show pop that mail status updated.
    }
    public void UpdateMailStatus(bool status)
    {
        emailData.isRead = status;
        UpdateUI();
    }
}
