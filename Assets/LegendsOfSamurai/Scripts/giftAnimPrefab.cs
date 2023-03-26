using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class giftAnimPrefab : MonoBehaviour
{
    internal void SetUI(int imageNo)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("MainInventoryItems/" + imageNo.ToString());
        //transform.DOMove(new Vector3(-574, 373, 0), 5f);
    }
  
}
