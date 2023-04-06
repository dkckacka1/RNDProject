using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Skill
{
    public abstract class Skill : MonoBehaviour
    {
        public int skillID;
        public string skillName;
        [Space()]
        [TextArea()]
        public string skillDesc;

        // 처음에 스킬이 어디서 나타날 것인가?
        public Vector3 skillStartPositionOffset;
    }
}