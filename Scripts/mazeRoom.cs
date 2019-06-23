using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeRoom : ScriptableObject
{
    public int settingsIndex;

    public mazeRoomSettings settings;

    private List<mazeCell> cells = new List<mazeCell>();

    public void Add (mazeCell cell)
    {
        cell.room = this;
        cells.Add(cell);
    }
}
