using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{ 
    /// <summary>
    /// 影响效果接口
    /// </summary>
    public interface IImpactEffect
    {
        //伤害生命
        void Execute(SkillDeployer deployer);
    }
}