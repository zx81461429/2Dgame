using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class LoadGame : MonoBehaviour
{
    public Iventory bag;
    public string jsonStr;


    //数据保存类
    public class LoadData
    {
        public ItemData[] item;
    }
    public void Load()
    {
        //创建保存类
        LoadData data = new LoadData();
        data.item = new ItemData[bag.itemlist.Count];

        //---------------------------------------------

        //物品的保存方法
        //背包保存
        for (int i = 0; i < bag.itemlist.Count; i++)
        {
            data.item[i] = bag.itemlist[i];
        }

        //---------------------------------------------

        // object -> json  序列化 以及创建流写入
        jsonStr = JsonUtility.ToJson(data);

        if (!File.Exists("data.json"))
            File.Create("data.json").Close();
        using (StreamWriter sw = new StreamWriter(new FileStream("data.json", FileMode.Truncate)))
        {
            sw.Write(jsonStr);
            Debug.Log("保存完毕");
            sw.Close();
        }
    }

    public void Read()
    {
        //创建流读取
        LoadData info = new LoadData();
        if (!File.Exists("data.json"))
        {
            Debug.Log("保存失败");
            return;
        }

        using (StreamReader sr = new StreamReader(new FileStream("data.json", FileMode.Open)))
        {

            string jsonStrRead = sr.ReadLine();
            //json -> object
            info = JsonUtility.FromJson<LoadData>(jsonStrRead);
            Debug.Log("读取完毕");
            sr.Close();
        }

        //---------------------------------------------

        //物品信息读取
        //背包读取
        for (int i = 0; i < info.item.Length; i++)
        {
            bag.itemlist[i] = info.item[i];
        }

        //---------------------------------------------

        //重新生成背包
        InventoryManager._instance.RefrashBag();
    }


}
