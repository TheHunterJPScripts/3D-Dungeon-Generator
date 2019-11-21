using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour {
    public DoorVisualization[] doors;
    public List<int> available_doors;

    private void Start() {
        available_doors = new List<int>();
        for (int i = 0; i < doors.Length; i++) {
            if (doors[i].enable) {
                available_doors.Add(i);
            }
        }
    }
    public enum DoorState { DoorAvailable = 0, DoorUsed = 1, LastDoorAvailable = 2, LastDoorUsed }; 
    public DoorState GetDoor(out Vector2Int result) {
        int available_index = Random.Range(0, available_doors.Count);
        int index = available_doors[available_index];
        DoorVisualization door = doors[index];
        Vector3 position = door.transform.position;

        available_doors.RemoveAt(available_index);
        door.enable = false;
        result = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        bool door_used = DungeonGenerator.indexFromPosition.ContainsKey(result.x.ToString() + "," + result.y.ToString());


        if (available_doors.Count == 0) {
            if (door_used) {
                return DoorState.LastDoorUsed;
            } else {
                return DoorState.LastDoorAvailable;
            }
        } else {
            if (door_used) {
                return DoorState.DoorUsed;
            } else {
                return DoorState.DoorAvailable;
            }
        }
    }
    public bool HaveDoor(string name) {
        return transform.Find(name) != null;
    }
}
