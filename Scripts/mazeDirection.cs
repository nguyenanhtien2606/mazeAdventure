using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mazeDirection
{
    North,
    East,
    South,
    West
}

public static class mazeDirections
{
    public const int Count = 4;

    public static mazeDirection RandomValue
    {
        get
        {
            return (mazeDirection)Random.Range(0, Count);
        }
    }

    private static IntVector2[] vectors = {                                 
        new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
    };

    public static IntVector2 ToIntVector2(this mazeDirection direction)
    {
        return vectors[(int)direction];
    }

    private static mazeDirection[] opposites = {
		mazeDirection.South,
		mazeDirection.West,
		mazeDirection.North,
		mazeDirection.East
	};

    public static mazeDirection GetOpposite(this mazeDirection direction)
    {
        return opposites[(int)direction];
    }

    private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};

    public static Quaternion ToRotation(this mazeDirection direction)
    {
        return rotations[(int)direction];
    }

    public static mazeDirection GetNextClockwise(this mazeDirection direction)
    {
        return (mazeDirection)(((int)direction + 1) % Count);
    }

    public static mazeDirection GetNextCounterclockwise(this mazeDirection direction)
    {
        return (mazeDirection)(((int)direction + Count - 1) % Count);
    }
}
