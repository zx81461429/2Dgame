using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ARPGDemo.Character
{ 
    /// <summary>
    /// 动画参数类
    /// </summary>
    [System.Serializable]
    public class CharacterAnimationParameter
    {
        public string run = "run";
        public string walk = "walk";
        public string idle = "idle";
        public string death = "death";
        public string damage = "damage";
        public string attack1 = "attack1";
        public string attack2 = "attack2";
        public string attack3 = "attack3";
    }
}