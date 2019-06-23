using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeWall : mazeCellEdge 
{
    public Transform wall;

    public override void Initialize(mazeCell cell, mazeCell otherCell, mazeDirection direction)
    {
        base.Initialize(cell, otherCell, direction);
        //wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
    }
}
