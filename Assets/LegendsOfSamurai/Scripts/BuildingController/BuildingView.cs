using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    BuildingController buildingController;

    private void Start()
    {
        Initialize("1");
    }


    void Initialize(string id)
    {
        //buildingController = new BuildingController(id,Vector2Int.zero,Vector2Int.zero);
    }
    
    
}


