using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance;
    public static List<Item> Items = new List<Item>();
    void Awake()
    {
        Instance = this;
    }

    public static void add(Item item)
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == item.itemName)
            {
                Items[i].number++;
                return;
            }
        }
        item.number = 1;
        Items.Add(item);
        Icon_Backpack.Instance.setNewMark();
        return;
    }

    public static void Remove(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == item.itemName)
            {
                Items[i].number--;
                if(Items[i].number == 0)
                    Items.RemoveAt(i);
                return;
            }
        }
        item.number = 0;
        Items.Remove(item);
        return;
    }
}
