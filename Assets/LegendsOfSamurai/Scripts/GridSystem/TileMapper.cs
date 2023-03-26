using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileMapper 
{
    // Start is called before the first frame update
    //public GameObject tile;
    //public Vector2 xy;

    //[ContextMenu("Map Tile")]
    //public void SetOnXy()
    //{
    //    tile.transform.position = GetWorldPosition(xy);
    //}
    public static Vector3 GetWorldPosition(Vector2 cell, bool turnOnZOrdering = false)
    {
        float posX = (cell.x  - cell.y) /2f ;
        float posY = (cell.x  + cell.y) /4f ;

        float buildingnum = cell.x * 16 + cell.y;
        buildingnum = (buildingnum / 256) * -1;
        buildingnum -= 0.001f;
        //Debug.Log("Before BuildingNum " + buildingnum);
        buildingnum = -1 + (buildingnum * -1);
        //Debug.Log("After BuildingNum " + buildingnum);
        if (turnOnZOrdering)
            return new Vector3(posX, posY, buildingnum);
        else
            return new Vector3(posX, posY, 0);

        //  return new Vector3(cell.x*0.5f - cell.y * 0.5f ,cell.y*0.25f + cell.x*0.25f,0);
    }
    public static Vector2Int GetTileCoordinatesFromPosition(Vector3 pos)
    {
        int posX = ((int)(2 * pos.y + pos.x));
        int posY = ((int)(2 * pos.y - pos.x));
        return new Vector2Int(posX, posY);
        //  return new Vector3(cell.x*0.5f - cell.y * 0.5f ,cell.y*0.25f + cell.x*0.25f,0);
    }
}
