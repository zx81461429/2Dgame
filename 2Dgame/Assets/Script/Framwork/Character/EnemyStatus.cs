using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    /// <summary>
    /// 敌人状态类
    /// </summary>
    public class EnemyStatus : CharacterStatus
    {
        public override void Dead()
        {
            base.Dead();
            Destroy(this, 5);
        }
    }
}
