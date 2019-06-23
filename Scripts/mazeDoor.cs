using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeDoor : mazePassage
{

    public Transform hinge;

    private mazeDoor OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as mazeDoor;
        }
    }

    public override void Initialize (mazeCell primary, mazeCell other, mazeDirection direction)
    {
        base.Initialize(primary, other, direction);

        if (OtherSideOfDoor != null)
        {
            hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child != hinge)
            {
                //child.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
            }
        }
    }
}
