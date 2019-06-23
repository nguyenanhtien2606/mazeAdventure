using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class mazeCellEdge : MonoBehaviour {

    public mazeCell cell, otherCell;

    public mazeDirection direction;

    public virtual void Initialize (mazeCell cell, mazeCell otherCell, mazeDirection direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}
