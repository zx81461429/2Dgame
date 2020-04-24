using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "new menu", menuName = "Menu/creat itemData")]
[Serializable]
public class ItemData : ScriptableObject
{
    public enum Type {
        equip,
        potion,
        material,
    }
    public enum Equip
    {
        wuqi,
        toukui,
        xiongjia,
        xiezi,
        shipin,
    }
    [SerializeField]
    public int id;//物品id
    public int num;//持有数量
    public string Name;//物品名称
    public Sprite img;//物品图片
    public Type type;//物品类型
    public Equip EquipType;//装备类型
    [TextArea]
    public string itemInfo;//物品描述
}


