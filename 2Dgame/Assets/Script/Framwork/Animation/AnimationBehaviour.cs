using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Anim.Behaviour
{
    /// <summary>
    /// 动画事件行为类
    /// </summary>
    public class AnimationBehaviour : MonoBehaviour
    {
        //脚本给到策划
        //为动画片段添加事件  指向OnAttack

        //程序  实现OnAttack

        //挂到当前具有Animator组件的游戏物体 


        //声明攻击事件
        public event Action attackHandler;
        Animator anim;
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        //攻击事件
        void OnAttack()
        {
            attackHandler?.Invoke();
        }

        //自动结束动画事件
        void OnCancelAnim(string name)
        {
            Debug.Log("结束");
            anim.SetBool(name, false);
        }
        private void OnAnimatorIK(int layerIndex)
        {
            
        }
    }
}