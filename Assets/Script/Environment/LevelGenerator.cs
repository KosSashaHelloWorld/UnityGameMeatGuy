using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int GenerateRoomChance = 50;
    public int RoomsMaxAmount = 20;
    private int RoomsCount = 0;
    public Room RoomPrefab;
    public Corridor CorridorPrefab;
    public Wall WallPrefab;
    public Room StartHub;
    private RoomWarehouse RoomWarehouse = new RoomWarehouse();


    void Start()
    {
        RoomWarehouse.AddOpenedExitPoints(StartHub);

        while (RoomWarehouse.NotEmpty())
        {
            GenerateChunk(RoomWarehouse.TakeRandomOpenedExitPoint());
        }
    }

    void GenerateChunk(Transform genPoint) 
    {
        if (RoomsCount >= RoomsMaxAmount || NotEnoughSpace(genPoint))
        {
            Instantiate(WallPrefab.gameObject, genPoint.position - WallPrefab.BASE.localPosition, genPoint.rotation * WallPrefab.BASE.rotation);
        }
        else
        {
            Corridor newCorridor = Instantiate(CorridorPrefab.gameObject,
             genPoint.position - CorridorPrefab.ENTRY_POINT.localPosition,
             genPoint.rotation).GetComponent<Corridor>();

            foreach (Transform corridorExit in newCorridor.EXIT_POINTS)
            {
                Room newRoom = Instantiate(RoomPrefab.gameObject,
                    corridorExit.position - RoomPrefab.ENTRY_POINT.localPosition,
                    corridorExit.rotation).GetComponent<Room>();
                RoomsCount++;

                RoomWarehouse.AddOpenedExitPoints(newRoom);
            }
        }
        
    }

    bool NotEnoughSpace(Transform genPoint)
    {
        var startPos = genPoint.position + genPoint.right;
        var endPos = genPoint.right * 30;
        Ray ray = new Ray(startPos, endPos);
        Debug.DrawRay(startPos, endPos, Color.blue);
        return Physics.Raycast(ray);
    }
}
