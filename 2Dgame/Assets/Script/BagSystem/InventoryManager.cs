using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;


    public Iventory myBag;
    public GameObject slotGrid; // 整个的方块
    public GameObject slotPrefab; //单个方块上的脚本
    public Text itemdescription;


    public List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }
    void Start()
    {
        InventoryManager._instance.RefrashBag();
    }

    //生成格子(第一版背包 已弃用)
    /*
    public void CreatNewSlot(ItemData item)
    {
        //实例化脚本也会生成对应的对象
        Griditem newitem = Instantiate(_instance.slotPrefab, _instance.slotGrid.transform.position, Quaternion.identity);
        newitem.transform.SetParent(_instance.slotGrid.transform);
        newitem.item = item;
        newitem.img.sprite = item.img;
        newitem.num.text = item.num.ToString();
    }
    */

    //重新生成背包
    public void RefrashBag()
    {
        for (int i = 0; i < slotGrid.transform.childCount; i++)
        {
            if (slotGrid.transform.childCount == 0)
                return;
            Destroy(slotGrid.transform.GetChild(i).gameObject);
            _instance.slots.Clear();
        }

        for (int i = 0; i < myBag.itemlist.Count; i++)
        {
            //CreatNewSlot(myBag.itemlist[i]);
            _instance.slots.Add(Instantiate(_instance.slotPrefab));
            _instance.slots[i].transform.SetParent(slotGrid.transform);//更改父物体有可能会使得生成物体 scale值变化
            _instance.slots[i].transform.localScale = new Vector3(1, 1, 1);
            _instance.slots[i].GetComponent<Griditem>().SlotID = i;
            _instance.slots[i].GetComponent<Griditem>().SetupSlot(_instance.myBag.itemlist[i]);
            if (myBag.itemlist[i] != null && myBag.itemlist[i].type == ItemData.Type.equip)
                _instance.slots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }       
    }

    public void AddNewItem(ItemData item, Iventory Playerinven)
    {
        if (!Playerinven.itemlist.Contains(item) || item.type == ItemData.Type.equip)
        {
            for (int i = 0; i < InventoryManager._instance.myBag.itemlist.Count; i++)
            {
                if (InventoryManager._instance.myBag.itemlist[i] == null)
                {
                    InventoryManager._instance.myBag.itemlist[i] = item;
                    InventoryManager._instance.myBag.itemlist[i].num++;
                    break;
                }
            }
        }
        else
        {
            item.num++;
        }
        InventoryManager._instance.RefrashBag();
    }

    public void ReduceItem(ItemData item, Iventory Playerinven)
    {
        if (item.num == 0)
            return;
        else if (item.num == 1 || item.type == ItemData.Type.equip)
        {
            for (int i = 0; i < InventoryManager._instance.myBag.itemlist.Count; i++)
            {
                if (InventoryManager._instance.myBag.itemlist[i] == item)
                {
                    InventoryManager._instance.myBag.itemlist[i].num--;
                    InventoryManager._instance.myBag.itemlist[i] = null;
                    break;
                }
            }
        }
        else
        {
            item.num--;
        }
        InventoryManager._instance.RefrashBag();
    }
}
