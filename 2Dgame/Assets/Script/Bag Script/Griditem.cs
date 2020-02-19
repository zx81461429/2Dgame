using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemData;
using UnityEngine.UI;

public class Griditem : MonoBehaviour
{
    public GameObject itemInslot; //slot 下挂载的 item
    public ItemData item;

    public Text num;//持有数量
    public Image img;//物品图片
    public ItemData.Type type;//物品类型

    public int SlotID;

    public void SetupSlot(ItemData item)
    {
        if (item == null)
        {
            itemInslot.SetActive(false);
            return;
        }
        this.item = item;
        num.text = item.num.ToString();
        img.sprite = item.img;
        type = item.type;
    }
}
