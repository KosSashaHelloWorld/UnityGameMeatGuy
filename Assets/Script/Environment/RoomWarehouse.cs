using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWarehouse
{
    private List<Transform> ExitPoints = new List<Transform>();

    public void AddOpenedExitPoints(Room room)
    {
        lock (this)
        {
            foreach (Transform point in room.EXIT_POINTS)
            {
                ExitPoints.Add(point);
            }
        }
    }

    public Transform TakeRandomOpenedExitPoint()
    {
        lock (this) 
        {
            int index = Random.Range(0, ExitPoints.Count);
            Transform point = ExitPoints[index];
            ExitPoints.RemoveAt(index);
            return point;
        }
    }

    public bool NotEmpty() 
    {
        return ExitPoints.Count != 0;
    }
}
