using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : WorldEntity
{
   
    BuildingsData buildingsData;

    public BuildingController(string id, Vector2Int pos, Vector2Int size ) : 
        base (id, pos.x,pos.y,size.x,size.y)
    {
       // buildingsData = DataManager.instance.buildingsData.Find(x => x._id == id);
    } 
}
