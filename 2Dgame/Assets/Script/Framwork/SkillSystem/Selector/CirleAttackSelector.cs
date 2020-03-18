using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo.Character;

namespace SkillSystem
{
    /// <summary>
    /// 扇形原型
    /// </summary>
    public class CirleAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData data, Transform TF)
        {
            //获取所有目标
            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {
                GameObject[] tempGoArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
                targets.AddRange(tempGoArray.SelectElement(x => x.transform));
            }

            //判断满足条件的  距离小于预定的距离
            targets = targets.FindAll(x => 
               Vector3.Distance(x.position, TF.position) <= data.attackDistance 
            && Vector3.Angle(TF.forward, x.position - TF.position) <= data.attackAngle / 2);

            //判断是活的
            targets.FindAll(x => x.GetComponent<CharacterStatus>().HP > 0);

            //单攻群攻
            if (data.attackType == SkillData.SkillAttackType.Many)
                return targets.ToArray();

            Transform min = targets.ToArray().GetMin(x => Vector3.Distance(TF.position, x.position));
            return new Transform[] { min };
        }
    }
}