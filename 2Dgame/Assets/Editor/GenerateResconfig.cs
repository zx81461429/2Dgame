using UnityEditor;
using System.IO;


public class GenerateResconfig : Editor
{
    [MenuItem("Tools/Resources/Generate Resconfig File")]
    public static void Generate()
    {
       //查找指定文件夹的路径 返回GUID
       string[] resFiles = AssetDatabase.FindAssets("t:prefab",new string[]{ "Assets/Resources" });

        //GUID转换为路径
        for (int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            //生成对应关系 名称==路径
            string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab",string.Empty);

            resFiles[i] = fileName + "=" + filePath;
        }

        if (File.Exists("Assets/StreamingAssets/PrefabData.txt"))
            File.Delete("Assets/StreamingAssets/PrefabData.txt");
        File.AppendAllLines("Assets/StreamingAssets/PrefabData.txt", resFiles);
        AssetDatabase.Refresh();
    }

}
