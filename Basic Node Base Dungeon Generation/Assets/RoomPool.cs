using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Room Pool")]
public class RoomPool : ScriptableObject
{
    public int index;
    public GameObject[] prefabs;
}
