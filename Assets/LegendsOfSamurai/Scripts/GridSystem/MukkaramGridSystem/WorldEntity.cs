using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntity 
{
        public string id;
        public int xPos;
        public int yPos;
        public int SizeX;
        public int SIzeY;

    public WorldEntity(string buildingid,int xPos,int yPos,int SizeX,int SizeY)
    {
        this.id = buildingid;
        this.xPos = xPos;
        this.yPos = yPos;
        this.SizeX = SizeX;
        this.SIzeY = SizeY;
    }
    public void RemoveEntity()
    {
         id = null;
        xPos = 0;
        yPos = 0;
        SizeX = 0;
        SIzeY = 0;
    }
}
