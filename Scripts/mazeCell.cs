using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeCell : MonoBehaviour {

    public IntVector2 coordinates;

    private int initializedEdgeCount;
    private mazeCellEdge[] edges = new mazeCellEdge[mazeDirections.Count];
    public mazeRoom room;

    public void Initialize (mazeRoom room)
    {
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
    }

    public bool IsFullyInitialized
    {
        get { return initializedEdgeCount == mazeDirections.Count; }
    }

    public mazeCellEdge GetEdge (mazeDirection direction)
    {
        return edges[(int)direction];
    }

    public void SetEdge(mazeDirection direction, mazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    public mazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, mazeDirections.Count - initializedEdgeCount);
            for(int i = 0; i < mazeDirections.Count; i++)
            {
                if(edges[i] == null)
                {
                    if(skips == 0)
                    {
                        return (mazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("mazeCell has no uninitialized directions left.");
        }
    }

}
