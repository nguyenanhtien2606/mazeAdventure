using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze : MonoBehaviour 
{
    public IntVector2 size;

    public mazeCell cellPrefab;
    private mazeCell[,] cells;
    public float generationStepDelay;
    public mazePassage passagePrefab;

    //public mazeDoor doorPrefab;
    [Range(0f, 1f)]
    public float doorProbability;

    public mazeWall[] wallPrefabs;
    public mazeRoomSettings[] roomSettings;
    private List<mazeRoom> rooms = new List<mazeRoom>();

    public IntVector2 RandomCoordinates 
    { 
        get 
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        } 
    }

    public bool ContainsCoordinates (IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    public mazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new mazeCell[size.x, size.z];
        List<mazeCell> activeCells = new List<mazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }


    private void DoFirstGenerationStep(List<mazeCell> activeCells)
    {
        mazeCell newCell = CreateCell(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }

    private void DoNextGenerationStep(List<mazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        mazeCell currentCell = activeCells[currentIndex];

        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }

        mazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            mazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private mazeCell CreateCell(IntVector2 coordinates)
    {
        mazeCell newCell = Instantiate(cellPrefab) as mazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition =
			new Vector3((coordinates.x - size.x * 0.5f + 0.5f)*10, 0f, (coordinates.z - size.z * 0.5f + 0.5f)*10);

        return newCell;
    }

    private void CreatePassage(mazeCell cell, mazeCell otherCell, mazeDirection direction)
    {
        //mazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;

        mazePassage prefab = Random.value < doorProbability ? passagePrefab : passagePrefab;
        mazePassage passage = Instantiate(passagePrefab) as mazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as mazePassage;
        if(passage is mazeDoor)
        {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else
        {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(mazeCell cell, mazeCell otherCell, mazeDirection direction)
    {
        mazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as mazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as mazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private mazeRoom CreateRoom(int indexToExclude)
    {
        mazeRoom newRoom = ScriptableObject.CreateInstance<mazeRoom>();
        newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
        if(newRoom.settingsIndex == indexToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }
}
