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

        // ó���� ��ų�� ��� ��Ÿ�� ���ΰ�?
        public Vector3 skillStartPositionOffset;
    }
}