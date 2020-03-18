using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{ 
    /// <summary>
    /// 攻击选区接口
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <param name="data">技能数据</param>
        /// <param name="TF">技能数据所在的组件</param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData data, Transform TF);
    }
}