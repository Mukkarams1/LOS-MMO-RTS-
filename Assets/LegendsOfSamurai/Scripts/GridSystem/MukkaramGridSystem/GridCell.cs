using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public int Xpos;
    public int Ypos;
    public string Tag;
    public bool IsWalkAble;
    public bool IsConstructable;
    public WorldEntity entity;

    public GridCell(int Xpos, int Ypos)
    {
        this.Xpos = Xpos;
        this.Ypos = Ypos;
    }

    public void SetIsWalkable(bool Iswalkable)
    {
        if (Iswalkable == false)
            this.IsWalkAble = false;
        else
        {
            this.IsWalkAble = true;
        }
    }
    public void SetCellTag(GameObject building, int buildingXpos, int buildingYpos)
    {
        if (Xpos == buildingXpos && Ypos == buildingYpos)
        {
            Tag = building.name;
        }
    }
    public void SetIsConstructable()
    {
        if (Tag == "" || Tag == "Empty")
        {
            IsConstructable = true;
        }
        else
        {
            IsConstructable = false;
        }
    }


}
