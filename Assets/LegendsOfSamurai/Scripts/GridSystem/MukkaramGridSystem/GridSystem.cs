using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    public GridCell[,] Grid;
    //public static GridSystem instance;
    Vector2Int dimensions;
    public Action<int,int> onCellCreated;
    public Action<int,int> onCellOcupied;

    
    public GridSystem(int x , int y)
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
        dimensions.x = x;
        dimensions.y = y;
        
       // CreatObject(null, new Vector2Int(2, 2), new Vector2Int(1, 1));
        
    }

    public Vector2Int GetEmptyCellXY(Vector2Int size)
    {
        
        for(int i = 0; i < dimensions.x - size.x; i++)
        {
            for (int j = 0; j < dimensions.y - size.y; j++)
            {
                if(IsConstructable(new Vector2Int(i, j), size))
                {
                    //SetNotConstructable(new Vector2Int(i, j), size);
                    return new Vector2Int(i, j);
                }
            } 
        }
        return new Vector2Int(2, 2);
    }

    private void SetNotConstructable(Vector2Int Pos, Vector2Int size)
    {
        for (int i = Pos.x; i < Pos.x + size.x; i++)
        {
            for (int j = Pos.y; j < Pos.y + size.y; j++)
            {

                Grid[i, j].IsConstructable = false;
            }
        }
    }

    private void CreatGridCell(int x, int y)
    {
        
        Grid = new GridCell[x,y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                onCellCreated?.Invoke(i, j);
               
                GridCell gridCell = new GridCell(i, j);
                gridCell.IsWalkAble = true;

                gridCell.IsConstructable = true;
                Grid[i,j] = gridCell;
            }

        }
        
    }
    void GetWorldPos()
    {
        //get world pos
    }


    public void CreatObject(WorldEntity entity, Vector2Int Pos, Vector2Int size)
    {
        for (int i = Pos.x; i < Pos.x + size.x ; i++)
        {
            for(int j = Pos.y; j < Pos.y + size.y; j++)
            {
                onCellOcupied?.Invoke(i, j);
                if (entity!= null)
                {
                    Grid[i, j].entity = entity;
                    Grid[i, j].Tag = entity.id;
                }
                Grid[i, j].IsWalkAble = false;
                Grid[i, j].IsConstructable = false;
                
            }
        }
    }

    internal void InitialiseGrid()
    {
        CreatGridCell(dimensions.x, dimensions.y);
        DrawGrid();
    }

    //RemoveObject at CellPos (new Vector2Int(35,35))
    public void RemoveObject(Vector2Int Pos,Vector2Int size)
    {
        if (Grid[Pos.x, Pos.y].entity != null)
        {
            for (int i = Pos.x; i < Pos.x + size.x; i++)
            {
                for (int j = Pos.y; j < Pos.y + size.y; j++)
                {
                    Grid[i, j].Tag = null;
                    Grid[i, j].IsWalkAble = true;
                    Grid[i, j].IsConstructable = true;
                    Grid[i, j].entity.RemoveEntity();
                }
            }
        }

    }

    public bool IsConstructable(Vector2Int Pos, Vector2Int size)
    {
        bool isConstructable = true;
        for (int i = Pos.x; i < Pos.x + size.x; i++)
        {
            for (int j = Pos.y; j < Pos.y + size.y; j++)
            {

                if (!isCellValid(Pos))
                {
                    isConstructable = false;
                    return isConstructable;
                }
                if(isCellValid(Pos))
                {
                    if (i < 31 && j < 31)
                    {
                        if (!Grid[i, j].IsConstructable)
                        {
                            isConstructable = false;
                            return isConstructable;
                        }
                    }
                    
                }
            }
        }
        return isConstructable;
    }

    public void DrawGrid()
    {
        for (int i = 0; i < dimensions.x; i++)
        {
            for (int j = 0; j < dimensions.y; j++)
            {
                DrawCell(new Vector2Int(i, j), Color.white);

            }
        }
    }

    void DrawCell(Vector2Int pos,Color color)
    {
        if (!Grid[pos.x, pos.y].IsWalkAble)
        {
            color = Color.red;
   
        }
        Debug.DrawLine(GetWorldPosition(pos), GetWorldPosition(pos + new Vector2Int(0, 1)), color, Mathf.Infinity);
        Debug.DrawLine(GetWorldPosition(pos), GetWorldPosition(pos + new Vector2Int(1, 0)), color, Mathf.Infinity);
        Debug.DrawLine(GetWorldPosition(pos + new Vector2Int(1, 0)), GetWorldPosition(pos + new Vector2Int(1, 1)), color, Mathf.Infinity);
        Debug.DrawLine(GetWorldPosition(pos + new Vector2Int(0, 1)), GetWorldPosition(pos + new Vector2Int(1, 1)), color, Mathf.Infinity);
    }

    public Vector3 GetWorldPosition(Vector2 xy)
    {
        return new Vector3(xy.x * 0.5f - xy.y * 0.5f, xy.y * 0.25f + xy.x * 0.25f, 0);
    }

    public bool isCellValid(Vector2Int Pos)
    {
        if (Pos.x >= 0 && Pos.x < dimensions.x-1 && Pos.y >= 0 && Pos.y < dimensions.y-1)
        {
            return true;
        }
        return false;
    }
    

}



