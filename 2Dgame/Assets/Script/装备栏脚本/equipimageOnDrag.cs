using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class equipimageOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    public Transform OldParent;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (Check())
        { return; }
        OldParent = transform.parent;
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (Check())
        { return; }
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (Check())
        { return; }
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //如果是空的
            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponent<Griditem>().SlotID] = transform.GetComponent<equip>().item;
                InventoryManager._instance.RefrashBag();
                EquipGenerate._instance.ReduceEquip(transform.GetComponent<equip>().item);
                Destroy(transform.GetChild(0).gameObject);
            }
            //替换装备 首先与物品类型都是装备 并且还是同一部位
            else if (eventData.pointerCurrentRaycast.gameObject.name == "item_image" &&
                    (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Griditem>().item.type == transform.GetComponent<equip>().item.type) &&
                    (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Griditem>().item.EquipType == transform.GetComponent<equip>().equiptype))
            {
                //装备栏对应位置生成图片
                GameObject pic = Instantiate(eventData.pointerCurrentRaycast.gameObject);
                pic.transform.SetParent(transform);
                pic.transform.position = transform.position;
                pic.transform.localScale = transform.localScale;
                //记录装备栏 的装备信息
                ItemData temp = transform.GetComponentInParent<equip>().item;
                //替换装备栏链表 背包中新佩戴的装备
                EquipGenerate._instance.SetEquip(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Griditem>().item, EquipGenerate._instance.myEquipBag);
                //替换背包中对应id 装备信息
                InventoryManager._instance.myBag.itemlist[eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponentInParent<Griditem>().SlotID] = temp;
                //销毁旧的装备图片
                Destroy(transform.GetChild(0).gameObject);
                //刷新背包
                InventoryManager._instance.RefrashBag();
            }
        }
        transform.position = OldParent.position;
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
    //当装备栏被点击时 被点击的是equip_slot
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            //都是装备不用判断了
            //直接将 点击的 装备 添加到背包链表中
            //先找到在装备栏的第几个被点击了
            int index = EquipGenerate._instance.getIndex(transform.GetComponent<equip>().item);
            //添加到背包里
            InventoryManager._instance.AddNewItem(EquipGenerate._instance.myEquipBag.itemlist[index], InventoryManager._instance.myBag);
            //让背包中的物品消失
            EquipGenerate._instance.myEquipBag.itemlist[index] = null;
            //刷新背包
            InventoryManager._instance.RefrashBag();
            //刷新装备栏
            Destroy(transform.GetChild(0).gameObject); 
        }

    }

}
