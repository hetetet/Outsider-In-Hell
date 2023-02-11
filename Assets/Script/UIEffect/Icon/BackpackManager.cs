using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance;
    public List<Item> Items=new List<Item> ();
    void Awake()
    {
        Instance = this;
    }

    public void add(Item item)
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == item.itemName)
            {
                Items[i].number++;
                return;
            }
        }
        Items.Add(item);
        return;
    }

    public void Remove(Item item)
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
        Items.Remove(item);
        return;
    }
}
