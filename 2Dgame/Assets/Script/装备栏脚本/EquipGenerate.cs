using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipGenerate : MonoBehaviour
{
    public Iventory myEquipBag;
    public GameObject wuqi;
    public GameObject toukui;
    public GameObject xiongjia;
    public GameObject xiezi;
    public GameObject shipin;
    public static EquipGenerate _instance;
    public Image img;

    public Dictionary<int, ItemData.Equip> dic = new Dictionary<int, ItemData.Equip>();
    void Start()
    {
        _instance = this;
        GenerateNew();

        dic.Add(0, ItemData.Equip.wuqi);
        dic.Add(1, ItemData.Equip.toukui);
        dic.Add(2, ItemData.Equip.xiongjia);
        dic.Add(3, ItemData.Equip.xiezi);
        dic.Add(4, ItemData.Equip.shipin);
    }

    public void Generate(GameObject equip, int index)
    {
        if (_instance.myEquipBag.itemlist[index] != null)
        {
            if (equip.transform.childCount == 1)
                Destroy(equip.transform.GetChild(0).gameObject);
            img.sprite = _instance.myEquipBag.itemlist[index].img;
            GameObject pic = Instantiate(img.gameObject);
            pic.transform.SetParent(equip.transform);
            pic.transform.position = equip.transform.position;
            pic.transform.localScale = wuqi.transform.localScale;
            pic.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);

            equip.GetComponent<equip>().type = ItemData.Type.equip;
            equip.GetComponent<equip>().equiptype = _instance.myEquipBag.itemlist[index].EquipType;
            equip.GetComponent<equip>().item = _instance.myEquipBag.itemlist[index];
        }
    }

    public void SetEquip(ItemData item, Iventory myequitBag)
    {
        switch (item.EquipType)
        {
            case ItemData.Equip.wuqi:
                myequitBag.itemlist[0] = item;
                wuqi.GetComponent<equip>().item = _instance.myEquipBag.itemlist[0];
                break;
            case ItemData.Equip.toukui:
                myequitBag.itemlist[1] = item;
                toukui.GetComponent<equip>().item = _instance.myEquipBag.itemlist[0];
                break;
            case ItemData.Equip.xiongjia:
                myequitBag.itemlist[2] = item;
                xiongjia.GetComponent<equip>().item = _instance.myEquipBag.itemlist[0];
                break;
            case ItemData.Equip.xiezi:
                myequitBag.itemlist[3] = item;
                xiezi.GetComponent<equip>().item = _instance.myEquipBag.itemlist[0];
                break;
            case ItemData.Equip.shipin:
                myequitBag.itemlist[4] = item;
                shipin.GetComponent<equip>().item = _instance.myEquipBag.itemlist[0];
                break;
        }
    }

    public void ReduceEquip(ItemData item)
    {
        for (int i = 0; i < _instance.myEquipBag.itemlist.Count; i++)
        {
            if (_instance.myEquipBag.itemlist[i] == null)
                continue;
            if (item.EquipType == _instance.myEquipBag.itemlist[i].EquipType)
            {
                _instance.myEquipBag.itemlist[i] = null;
                break;
            }
        }
    }

    public void GenerateNew()
    {
        Generate(wuqi, 0);
        Generate(toukui, 1);
        Generate(xiongjia, 2);
        Generate(xiezi, 3);
        Generate(shipin, 4);
    }


    public int getIndex(ItemData itam)
    {
        int index = -1;
        foreach (var i in dic)
        {
            if (itam.EquipType == i.Value)
            {
                index = i.Key;
                break;
            }
        }
        return index;
    }
}
