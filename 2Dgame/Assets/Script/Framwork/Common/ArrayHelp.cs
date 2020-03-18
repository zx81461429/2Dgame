using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 数组助手类
    /// </summary>
    public static class ArrayHelp
    {
        /* 7个方法 
         * 查找满足条件的(所有)对象
         * 排序【升序 降序】
         * 最大值  最小值
         * 筛选
         */
        /// <summary>
        /// 查找满足要求的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }
        /// <summary>
        /// 查找所有满足要求的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition">满足条件如果满足则为true</param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array, Func<T, bool> condition)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMax<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            if (array.Length == 0)
                return default(T);
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(max).CompareTo(condition(array[i])) < 0)
                {
                    max = array[i];
                }
            }
            return max;
        }
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMin<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            if (array.Length == 0)
                return default(T);
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(min).CompareTo(condition(array[i])) > 0)
                {
                    min = array[i];
                }
            }
            return min;
        }
        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T[] AsendSort<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            return array;
        }
        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T[] DsendSort<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) < 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            return array;
        }
        /// <summary>
        /// 筛选元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Q[] SelectElement<T, Q>(this T[] array,Func<T,Q> condition)
        {
            Q[] result = new Q[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = condition(array[i]);
            }
            return result;
        }
    }
}