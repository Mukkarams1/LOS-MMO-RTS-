using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPrefab : MonoBehaviour
{
    public ClanChatting chatData;
    public ChatPanel chatPanel1;
    public Text fromTxt;
    public Text msgTxt;

    public void SetChatDataforClanChat(ClanChatting data, ChatPanel chatPanel)
    {
        chatData = data;
        chatPanel1 = chatPanel;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(chatData.isMyself == false)
        {
            fromTxt.text = chatData.fromName;
        }
        msgTxt.text = chatData.message + "  -- " + chatData.createdAt;
    }
}
