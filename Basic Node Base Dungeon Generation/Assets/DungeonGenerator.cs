using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {
    [Range(0, 1)]
    public float secondPerRoom;
    public int averageRoomCount;
    public int roomCount;
    public float cellSize;
    private List<Rooms> roomPool;
    private List<int> openEnded;
    public static Dictionary<string, int> indexFromPosition;
    [Space(10)]

    public RoomPool[] prefabPool;

    private GameObject folder;

    public void GenerateCoroutine() {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine() {
        roomCount = 0;
        roomPool = new List<Rooms>();
        openEnded = new List<int>();
        indexFromPosition = new Dictionary<string, int>();
        if (folder != null) {
            GameObject.Destroy(folder);
        }
        do {
            GenerateRoom();
            yield return new WaitForSecondsRealtime(secondPerRoom);
        } while (openEnded.Count != 0);
    }


    public void GenerateRoom() {
        if (prefabPool == null || prefabPool.Length == 0) {
            throw new System.Exception("No prefabs for the rooms can be found");
        }

        if (roomPool == null) {
            roomPool = new List<Rooms>();
        }

        if (openEnded == null) {
            openEnded = new List<int>();
        }

        if (indexFromPosition == null) {
            indexFromPosition = new Dictionary<string, int>();
        }

        if (folder == null)
            folder = new GameObject();

        if (roomPool.Count == 0) {
            string key = "0,0";
            int index = roomPool.Count;
            GameObject new_room = Instantiate(SelectRoom(new Vector2Int(0, 0)));
            new_room.transform.position = Vector3.zero;
            new_room.transform.parent = folder.transform;
            openEnded.Add(index);
            indexFromPosition.Add(key, index);
            roomPool.Add(new_room.GetComponent<Rooms>());
            roomCount++;
        } else {
            int open_index = Random.Range(0, openEnded.Count);
            int room_index = openEnded[open_index];
            Rooms room = roomPool[room_index];
            Vector2Int position = new Vector2Int();

            Rooms.DoorState door_state = room.GetDoor(out position);

            if (door_state == Rooms.DoorState.DoorAvailable || Rooms.DoorState.LastDoorAvailable == door_state) {
                string key = position.x.ToString() + "," + position.y.ToString();
                GameObject new_room = Instantiate(SelectRoom(position));
                new_room.transform.position = new Vector3(position.x, 0, position.y);
                new_room.transform.parent = folder.transform;
                int index = roomPool.Count;
                openEnded.Add(index);
                indexFromPosition.Add(key, index);
                roomPool.Add(new_room.GetComponent<Rooms>());
                roomCount++;
            } 
            if (door_state == Rooms.DoorState.LastDoorAvailable || door_state == Rooms.DoorState.LastDoorUsed) {
                openEnded.RemoveAt(open_index);
            }
        }
    }

    GameObject SelectRoom(Vector2Int key) {
        if (indexFromPosition.Count == 0) {
            return prefabPool[0].prefabs[0];
        } else {
            int index = 0;
            int bytes = 0;

            string top = key.x.ToString() + "," + ( key.y + cellSize ).ToString();
            string down = key.x.ToString() + "," + ( key.y - cellSize ).ToString();
            string right = ( key.x + cellSize ).ToString() + "," + key.y.ToString();
            string left = ( key.x - cellSize ).ToString() + "," + key.y.ToString();

            if (indexFromPosition.TryGetValue(top, out index)) {
                Rooms room = roomPool[index];
                if (!room.HaveDoor("Down")) {
                    bytes += 8;
                }

            } else {
                if (Random.value > 0.5f || roomCount >= averageRoomCount) {
                    bytes += 8;
                }
            }

            if (indexFromPosition.TryGetValue(down, out index)) {
                Rooms room = roomPool[index];
                if (!room.HaveDoor("Top")) {
                    bytes += 4;
                }
            } else {
                if (Random.value > 0.5f || roomCount >= averageRoomCount) {
                    bytes += 4;
                }
            }

            if (indexFromPosition.TryGetValue(right, out index)) {
                Rooms room = roomPool[index];
                if (!room.HaveDoor("Left")) {
                    bytes += 2;
                }
            } else {
                if (Random.value > 0.5f || roomCount >= averageRoomCount) {
                    bytes += 2;
                }
            }

            if (indexFromPosition.TryGetValue(left, out index)) {
                Rooms room = roomPool[index];
                if (!room.HaveDoor("Right")) {
                    bytes += 1;
                }
            } else {
                if (Random.value > 0.5f || roomCount >= averageRoomCount) {
                    bytes += 1;
                }
            }
            return prefabPool[bytes].prefabs[0];
        }
    }
}
