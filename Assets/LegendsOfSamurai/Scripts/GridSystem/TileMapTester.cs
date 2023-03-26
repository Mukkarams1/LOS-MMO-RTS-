using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapTester : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tile;
    public Vector2 xy;
    public bool zOrdering;

    [ContextMenu("Map Tile")]
    public void SetOnXy()
    {
        tile.transform.position = GetWorldPosition(xy);
    }
    public Vector3 GetWorldPosition(Vector2 cell)
    {

        return TileMapper.GetWorldPosition(cell, zOrdering);
        //  return new Vector3(cell.x*0.5f - cell.y * 0.5f ,cell.y*0.25f + cell.x*0.25f,0);
    }
    public Vector2Int GetTileCoordinatesFromPosition(Vector3 pos)
    {
        int posX = ((int)(2 * pos.y + pos.x));
        int posY = ((int)(2 * pos.y - pos.x));
        return new Vector2Int(posX, posY);
        //  return new Vector3(cell.x*0.5f - cell.y * 0.5f ,cell.y*0.25f + cell.x*0.25f,0);
    }
}
