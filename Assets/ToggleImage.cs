using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    public Sprite SpriteOff;
    public Sprite SpriteOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle() {
        if (GetComponent<Image>().sprite == SpriteOff)
        {
            GetComponent<Image>().sprite = SpriteOn;
            AudioListener.pause = false;
        }
        else {
            GetComponent<Image>().sprite = SpriteOff;
            AudioListener.pause = true;
        }
    }
}
