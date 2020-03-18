using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{ 
    /// <summary>
    /// 技能释放配置工厂
    /// </summary>
    public class DeployerConfigFactory : MonoBehaviour
    {
        public static IAttackSelector CreatAttackSelector(SkillData skillData)
        {
            //选区算法对象
            //类名SkillSystem.+枚举名+AttackSelector
            string selectorName = string.Format("SkillSystem.{0}AttackSelector", skillData.selectorType);
            return CreateObject<IAttackSelector>(selectorName);
        }

        public static IImpactEffect[] CreatImpactEffect(SkillData skillData)
        {
            IImpactEffect[]  impactEffect = new IImpactEffect[skillData.impactType.Length];
            //影响算法对象
            //类名SkillSystem.+impactType[?]+Impact
            for (int i = 0; i < skillData.impactType.Length; i++)
            {
                string className = string.Format("SkillSystem.{0}Impact", skillData.impactType[i]);
                impactEffect[i] = CreateObject<IImpactEffect>(className);
            }
            return impactEffect;
        }


        private static T CreateObject<T>(string className) where T : class
        {
            Type type = Type.GetType(className);
            T t = Activator.CreateInstance(type) as T;
            return t;
        }
    }
}