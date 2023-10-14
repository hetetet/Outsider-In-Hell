using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item", menuName="Item/Create New Item")]

public class Item : ScriptableObject
{
    public int type; 
    //0: 일반 아이템(대개 튜토리얼용)
    //1x: 의상
    //    10: 모자
    //    11: 가면
    //    12: 목 장식(목걸이, 스카프 등)
    //20: 무기
    //30: 연장
    //40: 도구(사용 방향이 정해져 있지 않은 도구. 호루라기 등)
    //50: 음식(요리 또는 약물)
    //100: 사진
    
    public string key;
    public string itemName;
    public int number=0;
    public bool isUsing = false;
    public Sprite picture;
    public GameObject itemobj;
}
