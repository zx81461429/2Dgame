using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Inventory" , menuName = "Menu/Creat Inventory")]
public class Iventory : ScriptableObject
{
    public List<ItemData> itemlist = new List<ItemData>();
}
