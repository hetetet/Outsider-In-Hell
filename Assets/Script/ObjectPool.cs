using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public SerializableDictionary<string, Mission> Missions;
    public SerializableDictionary<string, Item> Items;
}
