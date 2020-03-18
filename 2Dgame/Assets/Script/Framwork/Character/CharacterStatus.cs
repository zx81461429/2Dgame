using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{ 
    /// <summary>
    /// 所有角色状态类
    /// </summary>
    public class CharacterStatus : MonoBehaviour
    {
        //动画参数
        public CharacterAnimationParameter chParams = new CharacterAnimationParameter();
        /// <summary>血量</summary>
        [Tooltip("血量")]
        public float HP;
        /// <summary>最大血量</summary>
        [Tooltip("最大血量")]
        public float maxHP;
        /// <summary>蓝量</summary>
        [Tooltip("蓝量")]
        public float MP;
        /// <summary>最大蓝量</summary>
        [Tooltip("最大蓝量")]
        public float maxMP;
        /// <summary>基础攻击力</summary>
        [Tooltip("基础攻击力")]
        public float baseATK;
        /// <summary>防御力</summary>
        [Tooltip("防御力")]
        public float defence;
        /// <summary>攻击间隔</summary>
        [Tooltip("攻击间隔")]
        public float attackInterval;
        /// <summary>攻击距离</summary>
        [Tooltip("攻击距离")]
        public float attackDistance;

        public void Damage(float val)
        {
            val -= defence;
            if (val < 0) return;
            HP -= val;
            if (HP <= 0)
                Dead();
        }
        public virtual void Dead()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
            print("游戏结束");
        }
    }
}