using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;

namespace SkillSystem
{
    /// <summary>
    /// 造成伤害
    /// </summary>
    public class DamageImpact : IImpactEffect
    {
        SkillData data;
        public void Execute(SkillDeployer deployer)
        {
            data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            //计算攻击间隔是否超出持续时间
            float atkTime = 0;
            //伤害目标生命
            do
            {
                OnceDamage();
                yield return new WaitForSeconds(data.atkInterval);
                atkTime += data.atkInterval;
                deployer.GalculateTargets();
            } while (atkTime < data.durationTime);
        }

        private void OnceDamage()
        {
            //实际攻击力 = 攻击比率*基础攻击力
            float atk = data.atkRatio * data.owner.GetComponent<CharacterStatus>().baseATK;
            Debug.Log(data.attackTargets.Length);
            if (data.attackTargets.Length == 0)
                return;
            for (int i = 0; i < data.attackTargets.Length; i++)
            {
                var status = data.attackTargets[i].GetComponent<CharacterStatus>();
                status.Damage(atk);
            }
            //创建攻击特效
        }
    }
}