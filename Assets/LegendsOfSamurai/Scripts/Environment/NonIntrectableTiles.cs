using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NonIntrectableTiles : MonoBehaviour
{
    public GameObject NonIntrectableTilesPrefab;
   // public Button CreateGrid;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.transform.childCount <= 0)
        {
            //GenerateGridTiles();
            CreatGridCell(28,28);
        }

    }
    public void GenerateGridTiles()
    {
        for (int x = -17; x < 32; x++)
        {
            for (int y = -17; y < 32; y++)
            {
                GameObject instantiatedTile = Instantiate(NonIntrectableTilesPrefab, gameObject.transform);
                Vector3 spawnPosition = TileMapper.GetWorldPosition(new Vector2(x, y));
                //spawnPosition.z = -0.25f;
                instantiatedTile.transform.position = spawnPosition;
                instantiatedTile.name = x + ", " + y;
                //GridCell cell = new GridCell(x,y);
                //tilesDictionary.Add(new Vector2(x,y),cell) ;
            }
        }
    }
    private void CreatGridCell(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject instantiatedTile = Instantiate(NonIntrectableTilesPrefab, gameObject.transform);
                Vector3 spawnPosition = TileMapper.GetWorldPosition(new Vector2(i, j));
                //spawnPosition.z = -0.25f;
                instantiatedTile.transform.position = spawnPosition;
                instantiatedTile.name = i + ", " + j;
                //GridCell cell = new GridCell(x,y);
                //tilesDictionary.Add(new Vector2(x,y),cell) ;
            }

        }

    }
}
