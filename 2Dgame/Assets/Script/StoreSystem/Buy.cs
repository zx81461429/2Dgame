using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour
{
    public ItemData item;
    public Iventory Iventory;
    public GameObject queren;

    public void BtnbuyClick()
    {
        queren.SetActive(true);
        queren.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        //钱减少
    }

    public void BtnOkClik()
    {
        InventoryManager._instance.AddNewItem(item, Iventory);
        queren.SetActive(false);
    }

    public void BtnNoClik()
    {
        queren.SetActive(false);
    }
}
