using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{ 
    /// <summary>
    /// 技能释放器
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;
        public SkillData SkillData 
        {
            get 
            {
                return skillData;
            }
            set
            {
                skillData = value;
            }
        }

        IAttackSelector selector;
        IImpactEffect[] impactEffect;
        //创建算法对象
        private void InitDeployer()
        {
            //选区算法对象
            //类名SkillSystem.+枚举名+AttackSelector
            selector = DeployerConfigFactory.CreatAttackSelector(skillData);

            //影响算法对象
            //类名SkillSystem.+impactType[?]+Impact
            impactEffect = DeployerConfigFactory.CreatImpactEffect(skillData);
        }
        //执行算法对象
        public void GalculateTargets()
        {
            InitDeployer();
            skillData.attackTargets = selector.SelectTarget(skillData,transform);

            foreach (var item in skillData.attackTargets)
            {
                print(item);
            }
        }

        public void ImpactTargets()
        {
            for (int i = 0; i < impactEffect.Length; i++)
            {
                impactEffect[i].Execute(this);
            }
        }
        //释放方式
        public abstract void DeploySkill();
    }
}