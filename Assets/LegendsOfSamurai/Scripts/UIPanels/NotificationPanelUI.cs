using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationPanelUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI notificationText;
  
    public void SetNotificationText( string text)
    {
        notificationText.text = text;   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
