using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class userTroopEntry : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TroopName;

    [SerializeField]
    TextMeshProUGUI TroopQTY;

    // Update is called once per frame
  public void setText(string name, string qty, string qty2)
    {
        TroopName.text = name;
        TroopQTY.text = "Quantity: " + qty + " / In Queue " + qty2;    
    }
}
