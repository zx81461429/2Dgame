using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    public Transform OldParent;
    public int currentID;
    public GameObject equitImage;//状态栏
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Check())
        { return; }

        OldParent = transform.parent;
        currentID = transform.GetComponentInParent<Griditem>().SlotID; //获取当前slot id
        transform.SetParent(transform.parent.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//不检测自己本身
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Check())
        { return; }
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        else
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Check())
        { return; }
        //不为空 则在ui上
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //格自里有东西
            if (eventData.pointerCurrentRaycast.gameObject.name == "item_image")
            {
                //3.更新背包链表 (换完位置可能导致鼠标点击的itemID改变 所以要先更新背包链接)
                ItemData temp = InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponentInParent<Griditem>().SlotID];
                InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponentInParent<Griditem>().SlotID] = InventoryManager._instance.myBag.itemlist[currentID];
                InventoryManager._instance.myBag.itemlist[currentID] = temp;
                //4.更新两个格子的属性
                InventoryManager._instance.slots[currentID].GetComponent<Griditem>().item = InventoryManager._instance.myBag.itemlist[currentID];
                InventoryManager._instance.slots[eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponentInParent<Griditem>().SlotID].GetComponent<Griditem>().item = InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponentInParent<Griditem>().SlotID];
                InventoryManager._instance.slots[currentID].GetComponent<Griditem>().type = InventoryManager._instance.myBag.itemlist[currentID].type;
                InventoryManager._instance.slots[eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponentInParent<Griditem>().SlotID].GetComponent<Griditem>().type = InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponentInParent<Griditem>().SlotID].type;
                //1.设置当前item 父物体与位置
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //2.设置目标item 父物体与位置
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(OldParent);
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = OldParent.position;

            }
            //格子里东西是自己
            else if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                //如果放会到自己的地方
                if (eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Griditem>().SlotID == currentID)
                {
                    transform.SetParent(OldParent);
                    transform.position = OldParent.position;
                }
                else
                {
                    //更新背包链表
                    InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Griditem>().SlotID] = InventoryManager._instance.myBag.itemlist[currentID];
                    InventoryManager._instance.myBag.itemlist[currentID] = null;
                    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                    transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                }
            }
            //进入装备栏 并且 物品类型都是装备  并且是同一种部位
            else if (eventData.pointerCurrentRaycast.gameObject.name == "equip_slot" && 
                    (eventData.pointerCurrentRaycast.gameObject.GetComponent<equip>().type == OldParent.GetComponent<Griditem>().type) && 
                    (eventData.pointerCurrentRaycast.gameObject.GetComponent<equip>().equiptype == OldParent.GetComponent<Griditem>().item.EquipType))
            {
                //装备栏对应位置生成图片
                GameObject pic = Instantiate(this.transform.GetChild(0).gameObject);
                pic.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                pic.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                pic.transform.localScale = eventData.pointerCurrentRaycast.gameObject.transform.localScale;
                //记录旧的背包里 物品数据item
                eventData.pointerCurrentRaycast.gameObject.GetComponent<equip>().item = OldParent.GetComponent<Griditem>().item;
                Debug.Log(eventData.pointerCurrentRaycast.gameObject.GetComponent<equip>().item);
                //增加装备栏链表中的装备
                EquipGenerate._instance.SetEquip(OldParent.GetComponent<Griditem>().item, EquipGenerate._instance.myEquipBag);
                //删除背包链表中的装备
                InventoryManager._instance.myBag.itemlist[OldParent.GetComponentInParent<Griditem>().SlotID] = null;
                Destroy(this.gameObject);
            }
            else if ((eventData.pointerCurrentRaycast.gameObject.name == "Image(Clone)" || eventData.pointerCurrentRaycast.gameObject.name == "item_image(Clone)") && 
                    (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<equip>().type == OldParent.GetComponent<Griditem>().type) && 
                    (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<equip>().equiptype == OldParent.GetComponent<Griditem>().item.EquipType))
            {
                //装备栏对应位置生成图片
                GameObject pic = Instantiate(this.transform.GetChild(0).gameObject);
                pic.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
                pic.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                pic.transform.localScale = eventData.pointerCurrentRaycast.gameObject.transform.localScale;
                //记录装备栏 的装备信息
                ItemData temp = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<equip>().item;
                //替换装备栏链表 背包中新佩戴的装备
                EquipGenerate._instance.SetEquip(OldParent.GetComponent<Griditem>().item, EquipGenerate._instance.myEquipBag);
                //替换背包中对应id 装备信息
                InventoryManager._instance.myBag.itemlist[OldParent.GetComponentInParent<Griditem>().SlotID] = temp;
                //销毁旧的装备图片
                Destroy(eventData.pointerCurrentRaycast.gameObject);
                //销毁移过去的item物体
                Destroy(this.gameObject);
                //刷新背包
                InventoryManager._instance.RefrashBag();
            }
            //其他地方
            else
            {
                transform.SetParent(OldParent);
                transform.position = OldParent.position;
            }
        }
        //不在UI上
        else 
        {
            Destroy(transform.gameObject);
            InventoryManager._instance.myBag.itemlist[OldParent.GetComponentInParent<Griditem>().SlotID] = null;
            //transform.SetParent(OldParent);
            //transform.position = OldParent.position;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public bool Check()
    {
        if (Input.GetMouseButton(1) || 
            Input.GetMouseButton(2) || 
            Input.GetMouseButtonDown(1) || 
            Input.GetMouseButtonDown(2) || 
            Input.GetMouseButtonUp(1) || 
            Input.GetMouseButtonUp(2))
            return true;
        else
            return false;
    }

    //鼠标点击穿戴装备 脚本挂在item上
    public void OnPointerClick(PointerEventData eventData)
    {
        if (CheckBtn())
        { return; }
        if (transform.parent != null)
        {
            //当被点击的时候 判断物品 类型 如果是 装备 则执行下一步
            if (transform.parent.GetComponent<Griditem>().type == ItemData.Type.equip)
            {
                //找到字典中对应的类型位置
                int index = 0;
                foreach (var i in EquipGenerate._instance.dic)
                {
                    if (i.Value == transform.GetComponentInParent<Griditem>().item.EquipType)
                    {
                        index = i.Key;
                        break;
                    }
                }
                //判断装备栏链表 对应位置是否为空
                if (EquipGenerate._instance.myEquipBag.itemlist[index] != null)
                {
                    //不为空
                    //装备栏链表 和 背包链表 对应位置替换
                    ItemData temp = EquipGenerate._instance.myEquipBag.itemlist[index];
                    EquipGenerate._instance.myEquipBag.itemlist[index] = InventoryManager._instance.myBag.itemlist[transform.GetComponentInParent<Griditem>().SlotID];
                    InventoryManager._instance.myBag.itemlist[transform.GetComponentInParent<Griditem>().SlotID] = temp;
                    //刷新装备栏链表物品
                    EquipGenerate._instance.GenerateNew();
                    //刷新背包物品
                    InventoryManager._instance.RefrashBag();
                    //销毁以前装备栏的图片
                }
                else
                {
                    //为空
                    //增加装备栏链表物品
                    EquipGenerate._instance.SetEquip(transform.GetComponentInParent<Griditem>().item, EquipGenerate._instance.myEquipBag);
                    //刷新装备栏链表物品
                    EquipGenerate._instance.GenerateNew();
                    //删除背包链表物品
                    InventoryManager._instance.myBag.itemlist[transform.GetComponentInParent<Griditem>().SlotID] = null;
                    //刷新背包物品
                    InventoryManager._instance.RefrashBag();
                }
            }
        }

    }

    public bool CheckBtn()
    {
        if (Input.GetMouseButton(0) ||
            Input.GetMouseButton(2) ||
            Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(2) ||
            Input.GetMouseButtonUp(0) ||
            Input.GetMouseButtonUp(2))
            return true;
        else
            return false;
    }
}
