using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EmailPanelUi : MonoBehaviour
{
    public static EmailPanelUi instance;
    public Text descriptionTxt;
    public Text createdAtTxt;
    public Transform SubjectPlaceHolder;
    public GameObject SubjectPrefab;
    public Button linkBtn;
    public Button Closebtn;
    string SelectedMailId;
    string SelectedEmailLink;

    List<Subjectname> emailSubjectsList;

    internal void UpdatePanelDataInUI(Mail mailData)
    {
            linkBtn.gameObject.SetActive(mailData.link != null && mailData.link != String.Empty);
        descriptionTxt.text = mailData.description;

        createdAtTxt.text = mailData.createdAt.ToString("hh:mm tt, MMM dd, yyyy");

        SelectedMailId = mailData._id;
        SelectedEmailLink = (mailData.link != null && mailData.link != String.Empty)? mailData.link : String.Empty;
        

    }
    private void Start()
    {
        Closebtn.onClick.AddListener(OnClickClose);
        linkBtn.onClick.AddListener(OnLinkBtnClicked);

    }

    private void OnEnable()
    {
        emailSubjectsList = new List<Subjectname>();
        for (int j = 0; j < DataManager.instance.SignInResponseData.data.mails.Count; j++)
        {
            Subjectname subjectObjectBtn = Instantiate(SubjectPrefab, SubjectPlaceHolder).GetComponent<Subjectname>();
            subjectObjectBtn.SetEmailData(DataManager.instance.SignInResponseData.data.mails[j], this);
            emailSubjectsList.Add(subjectObjectBtn);
        }
    }
    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }
    private void OnLinkBtnClicked()
    {
        if (SelectedEmailLink != null || SelectedEmailLink == "")
        {
            Application.OpenURL(SelectedEmailLink);
        }
        SoundManager.instance.PlaySound("ButtonClick");
    }

    public void SetUIDetails(string from, string to, string subject, string description, string createdAt)
    {

        descriptionTxt.text = description.ToString();
        createdAtTxt.text = createdAt.ToString();
    }

    private void OnDisable()
    {
        descriptionTxt.text = String.Empty;
        createdAtTxt.text = String.Empty;
        linkBtn.gameObject.SetActive(false);

        foreach (Transform child in SubjectPlaceHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
