using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    public GameObject myChatPrefab;
    public GameObject senderChatPrefab;
    public Transform placeHolder;
    public Button Closebtn;
    private void Start()
    {
        Closebtn.onClick.AddListener(OnClickClose);
        SetChatUIForClanChat();
    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }

    void SetChatUIForClanChat()
    {
        for(int i = 0; i < DataManager.instance.chatData.ClanChatting.Length; i++)
        {
            if(DataManager.instance.chatData.ClanChatting[i].isMyself == true)
            {
                ChatPrefab Subobject =  Instantiate(myChatPrefab, placeHolder).GetComponent<ChatPrefab>();
                Subobject.SetChatDataforClanChat(DataManager.instance.chatData.ClanChatting[i], this);
            }
            else
            {
                ChatPrefab Subobject = Instantiate(senderChatPrefab, placeHolder).GetComponent<ChatPrefab>();
                Subobject.SetChatDataforClanChat(DataManager.instance.chatData.ClanChatting[i], this);
            }
        }
    }
}
