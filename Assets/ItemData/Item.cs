using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item", menuName="Item/Create New Item")]

public class Item : ScriptableObject
{
    public int type; 
    //0: �Ϲ� ������
    //1x: �ǻ�
    //    10: ����
    //    11: ����
    //    12: �� ���(�����, ��ī�� ��)
    //2: ����
    //3: ����
    public string key;
    public string itemName;
    public int number=1;
    public bool isUsing = false;
    public Sprite picture;
    public GameObject itemobj;
}
