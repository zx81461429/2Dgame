using ARPGDemo.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    /// <summary>
    /// 消耗SP
    /// </summary>
    public class CostSPImpact : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            var status = deployer.SkillData.owner.GetComponent<CharacterStatus>();
            status.MP -= deployer.SkillData.costSP;
        }
    }
}