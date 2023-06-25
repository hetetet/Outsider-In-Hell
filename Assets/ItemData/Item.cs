using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item", menuName="Item/Create New Item")]

public class Item : ScriptableObject
{
    public int type; 
    //0: 일반 아이템
    //1x: 의상
    //    10: 모자
    //    11: 가면
    //    12: 목 장식(목걸이, 스카프 등)
    //2: 무기
    //3: 사진
    public string key;
    public string itemName;
    public int number=1;
    public bool isUsing = false;
    public Sprite picture;
    public GameObject itemobj;
}
