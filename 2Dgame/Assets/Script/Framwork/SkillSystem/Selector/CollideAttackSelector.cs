using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace SkillSystem
{
    /// <summary>
    /// 碰撞选区
    /// </summary>
    public class CollideAttackSelector : IAttackSelector
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
            return targets.ToArray();
        }
    }
}

