using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item", menuName="Item/Create New Item")]

public class Item : ScriptableObject
{
    public int type; //0: 일반 아이템 1: 의상 2: 무기
    public string key;
    public string itemName;
    public int number=1;
    public Sprite picture;
}
