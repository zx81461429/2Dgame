using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{ 
    /// <summary>
    /// 技能数据类
    /// </summary>
    [Serializable]
    public class SkillData
    {
        /// <summary>技能ID</summary>
        public int skillID;
        /// <summary>技能名称</summary>
        public string name;
        /// <summary>技能描述</summary>
        public string discription;
        /// <summary>冷却时间</summary>
        public int coolTime;
        /// <summary>冷却剩余时间</summary>
        public int coolRemain;
        /// <summary>消耗蓝量</summary>
        public int costSP;
        /// <summary>攻击距离</summary>
        public float attackDistance;
        /// <summary>攻击角度</summary>
        public float attackAngle;
        /// <summary>目标tag</summary>
        public string[] attackTargetTags = {"Enemy"};
        /// <summary>攻击目标数组</summary>
        [HideInInspector]
        public Transform[] attackTargets;
        /// <summary>技能影响类型</summary>
        public string[] impactType = {"CostSp","Damage"};
        /// <summary>连击的下一个技能编号</summary>
        public int nextBatterld;
        /// <summary>伤害比率</summary>
        public float atkRatio;
        /// <summary>持续时间</summary>
        public float durationTime;
        /// <summary>攻击间隔</summary>
        public int atkInterval;
        /// <summary>技能所属</summary>
        [HideInInspector]
        public GameObject owner;
        /// <summary>预制件名称</summary>
        public string prefabName;
        /// <summary>预制件对象</summary>
        [HideInInspector]
        public GameObject skillPrefab;
        /// <summary>动画名称</summary>
        public string animationName;
        /// <summary>受击特效名称</summary>
        public string hitFxName;
        /// <summary>受击特效预制件</summary>
        [HideInInspector]
        public GameObject hitFxPrefab;
        /// <summary>技能等级</summary>
        public int level;
        /// <summary>攻击类型 单攻 群攻</summary>
        public SkillAttackType attackType;
        /// <summary>选择类型 扇形 矩形</summary>
        public SelectorType selectorType;

        public enum SkillAttackType { Single,Many }
        public enum SelectorType { Cirle, Fang,Collide }
    }
}