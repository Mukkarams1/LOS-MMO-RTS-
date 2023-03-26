using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructPanelController : MonoBehaviour
{
    public GameObject ConstructBuildingPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTapped() {
        //GameObject.Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
    public void ConstructTapped() {
        // GameObject.Destroy(this.gameObject);
        gameObject.SetActive(false);
        UIController.instance.ShowCreateBuildingPanel();
    }
}
