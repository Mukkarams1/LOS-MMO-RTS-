using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectionScript : MonoBehaviour
{
    // Declaration
    public Button button1, button2, button3;

    // Changing outline
    public void onHeroSelect(){
        button1.GetComponent<Outline>().enabled = true;
        button2.GetComponent<Outline>().enabled = false;
        button3.GetComponent<Outline>().enabled = false;
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
