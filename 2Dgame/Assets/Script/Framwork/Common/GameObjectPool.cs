using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{ 
    /// <summary>
    /// 对象池
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        //如需每次创建对象时执行的逻辑 请定义接口 在游戏物体上实现接口方法 并在UseObject最后 调用接口方法

        private Dictionary<string,List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        //从对象池中获取元素 重载1
        public GameObject CreateGameObject(string key, GameObject obj, Vector3 pos,Vector3 size , Quaternion quaternion)
        {
            GameObject go = null;
            //判断Hierarchy面板中是否有禁用的对象 有 就直接用
            go = FindUseAbleObj(key);
            //如果没有 则需要新创建对象
            if (go == null)
                go = AddObject(key, obj);

            UseObject(pos, size, quaternion, go);

            return go;
        }

        //重载2 可以设置父物体
        public GameObject CreateGameObject(string key, GameObject obj, Vector3 pos, Vector3 size, Quaternion quaternion, Transform parent)
        {
            GameObject go = null;
            //判断Hierarchy面板中是否有禁用的对象 有 就直接用
            go = FindUseAbleObj(key);
            //如果没有 则需要新创建对象
            if (go == null)
                go = AddObject(key, obj);

            UseObject(pos, size, quaternion, go);

            return go;
        }

        private void UseObject(Vector3 pos, Vector3 size, Quaternion quaternion, GameObject go)
        {
            //设置
            go.transform.position = pos;
            go.transform.localScale = size;
            go.transform.rotation = quaternion;
            go.SetActive(true);
        }

        private GameObject AddObject(string key, GameObject obj)
        {
            GameObject go = Instantiate(obj);
            //判断是否创建的物品 有所对应的池子 如果没有 就新建一个
            if (!cache.ContainsKey(key))
                cache.Add(key, new List<GameObject>());
            //无论有没有池子 都需要将新创建的物品添加进池子
            cache[key].Add(go);
            return go;
        }

        //查找Hierarachy中可用对象 (被禁用的)
        private GameObject FindUseAbleObj(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;
        }

        public void CollectObject(GameObject go,float delay = 0)
        {
            print(111);
            StartCoroutine(CollectObjectDelay(go, delay));
        }

        //删除一个对象池
        public void Clear(string key)
        {
            for (int i = cache[key].Count - 1; i >= 0 ; i--)
            {
                Destroy(cache[key][i]);
            }
            cache.Remove(key);
        }

        //删除所有对象池
        public void ClearAll()
        {
            foreach (var key in new List<string>(cache.Keys))
            {
                Clear(key);
            }
        }

        public IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }
    }
}