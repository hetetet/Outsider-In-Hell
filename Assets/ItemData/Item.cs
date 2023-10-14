using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item", menuName="Item/Create New Item")]

public class Item : ScriptableObject
{
    public int type; 
    //0: �Ϲ� ������(�밳 Ʃ�丮���)
    //1x: �ǻ�
    //    10: ����
    //    11: ����
    //    12: �� ���(�����, ��ī�� ��)
    //20: ����
    //30: ����
    //40: ����(��� ������ ������ ���� ���� ����. ȣ���� ��)
    //50: ����(�丮 �Ǵ� �๰)
    //100: ����
    
    public string key;
    public string itemName;
    public int number=0;
    public bool isUsing = false;
    public Sprite picture;
    public GameObject itemobj;
}
