using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    private void OnMouseUp()
    {
        
        Pickup();
    }

    public void Pickup()
    {
        Icon_Backpack.Instance.add(item);
        Destroy(gameObject);
    }
}
