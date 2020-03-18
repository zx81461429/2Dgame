using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo.Character;

namespace SkillSystem
{ 
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class CharacterSkillManager : MonoBehaviour
    {
        public SkillData[] skills;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
                InitSkill(skills[i]);
        }
        /// <summary>
        /// 初始化技能
        /// </summary>
        /// <param name="data"></param>
        private void InitSkill(SkillData data)
        {
            //data.prefabName -> data.skillprefab
            data.skillPrefab = ResourseManager.Load<GameObject>(data.prefabName);
            data.owner = gameObject;
        }

        public SkillData PrepareSkill(int id)
        {
            SkillData data = skills.Find<SkillData>(x => x.skillID == id);
            //不为空并且剩余冷却时间为0 并且蓝够
            if (data != null && data.coolRemain == 0 && data.costSP <= GetComponent<CharacterStatus>().MP)
                return data;
            return null;
        }

        //生成技能
        public void GenerateSkill(SkillData data)
        {
            //GameObject skillGo = Instantiate(data.skillPrefab, transform.position, Quaternion.identity);
            GameObject skillGo = GameObjectPool.Instance.CreateGameObject(data.prefabName, data.skillPrefab, transform.position, data.skillPrefab.transform.localScale, Quaternion.identity);

            SkillDeployer skillDeployer = skillGo.GetComponent<SkillDeployer>();
            skillDeployer.SkillData = data;

            skillDeployer.DeploySkill();

            GameObjectPool.Instance.CollectObject(skillGo, data.durationTime);

            //冷却
            StartCoroutine(CoolTimeDown(data));
        }

        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.coolRemain = data.coolTime;

            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
        }
    }
}