using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickAnimation : MonoBehaviour
{
    public int tester = 1;
    void OnMouseDown()
    {
        // Debug.Log("Click works");
        tester++;
    }
}
