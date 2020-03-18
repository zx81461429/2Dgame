using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SkillSystem
{ 
    [RequireComponent(typeof(CharacterSkillManager))]
    /// <summary>
    /// 封装技能系统
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        CharacterSkillManager skillManager;
        Animator anim;


        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();
            
        }
        public void AttackUseSkill(int skillId)
        {
            //准备技能
            SkillData data = skillManager.PrepareSkill(skillId);
            //播放动画
            if (data == null) return;
            //anim.SetBool(data.animationName, true);
            //生成技能
            //应该用动画事件 调用 生成技能 但是这里没有动画事件所以直接生成
            skillManager.GenerateSkill(data);
            //单攻
            //朝向目标 向目标移动 选中目标

            //Transform targetTF = SelectTarget(data);
            //transform.LookAt(targetTF);

            //选中后 敌人身上的XXX东西 setActive 就可以让敌人高亮 重新写一个类 这个类只负责调用技能 懒得写了
        }

        private Transform SelectTarget(SkillData data)
        {
            Transform[] target = new CirleAttackSelector().SelectTarget(data, transform);
            return target.Length != 0 ? target[0] : null;
        }



        //NPC
        public void UseRandomSkill()
        {
            var usableSkills = skillManager.skills.FindAll(s=>skillManager.PrepareSkill(s.skillID) != null);
            if (usableSkills.Length == 0) return;
            int index = UnityEngine.Random.Range(0, usableSkills.Length);
            AttackUseSkill(usableSkills[index].skillID);
        }
    }
}