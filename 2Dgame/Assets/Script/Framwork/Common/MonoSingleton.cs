using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{ 
    /// <summary>
    /// 脚本单例类
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
    {
        private static T instance;
        public static T Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        new GameObject("Singleton of" + typeof(T)).AddComponent<T>();
                    }
                    instance.Init();
                }
                return instance;
            }
        }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
            
        }

        public virtual void Init(){ }
    }
}