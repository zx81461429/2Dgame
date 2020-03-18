using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{ 
    /// <summary>
    /// 变换组件助手类
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// 未知层级 查找子物体游戏对象变换组件
        /// </summary>
        /// <param name="currentTF">需要查找的父物体</param>
        /// <param name="childName">子物体的名字</param>
        /// <returns>返回变换组件</returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            Transform childTF = currentTF.Find(childName);
            if (childTF != null)return childTF;
            for (int i = 0; i < currentTF.childCount; i++)
            {
                childTF  = FindChildByName(currentTF.GetChild(i), childName);
                if (childTF != null)return childTF;
            }
            return null;
        }
    }
}