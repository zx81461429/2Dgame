using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemonword : MonoBehaviour
{
    public ItemData item;
    public Iventory Playerinven;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InventoryManager._instance.AddNewItem(item,Playerinven);
            Destroy(gameObject);
        }
    }
}
