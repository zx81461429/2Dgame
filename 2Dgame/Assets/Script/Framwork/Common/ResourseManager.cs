using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourseManager
    {
        private static Dictionary<string, string> configMap;
        //静态构造函数 在类被加载的时候 进行操作 而且只做一遍 初始化所有静态成员
        static ResourseManager()
        {
            //加载文件
            string fileContent = GetConfigFile("PrefabData.txt");
            //解析文件
            BiuldMap(fileContent);
        }


        /// <summary>
        /// 加载文件
        /// </summary>
        /// <returns></returns>
        public static string GetConfigFile(string fileName)
        {
            string url;
            //string url = "file:///" + Application.streamingAssetsPath + "/fileName";
            //PC
            //if(Application.platform == RuntimePlatform.WindowsEditor)
#if UNITY_EDITOR
            url = "file:///" + Application.dataPath + "/StreamingAssets/" + fileName;
            //IOS
#elif UNITY_IPHONE
            url = "file:///" + Application.dataPath + "/Raw/PrefabData.txt";
            //Android
#elif UNITY_ANDROID
            url = "jar:file://" + Application.dataPath + "!/assets/PrefabData.txt";
#endif

            WWW www = new WWW(url);
            while (true)
            {
                if (www.isDone)
                    return www.text;
            }
        }

        /// <summary>
        /// 解析文件
        /// </summary>
        /// <param name="fileText"></param>
        /// <returns></returns>
        public static void BiuldMap(string fileText)
        {
            configMap = new Dictionary<string, string>();

            using (StringReader stringReader = new StringReader(fileText))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    string[] KeyValue = line.Split('=');
                    configMap.Add(KeyValue[0], KeyValue[1]);
                }
            }
            //StringReader类
        }

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Load<T>(string name) where T : Object
        {
            //Name => Path

            string prefabPath = configMap[name];
            T t =  Resources.Load<T>(prefabPath);
            return t;
        }
    }
}
