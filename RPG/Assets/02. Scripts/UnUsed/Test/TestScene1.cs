using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;
using RPG.Battle.UI;
using RPG.Battle.Control;

namespace RPG.Core
{
    public class TestScene1 : MonoBehaviour
    {
        public Animator animator;

        private void Start()
        {

        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("Idle");
            }

            if (GUI.Button(new Rect(10, 80, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("TemptationWalk");
            }

            if (GUI.Button(new Rect(10, 140, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("TemptationWalk");
            }

            if (GUI.Button(new Rect(10, 220, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("Stern");
            }

            if (GUI.Button(new Rect(10, 300, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("TemptationWalk");
            }

            if (GUI.Button(new Rect(10, 380, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("FearRun");
            }

            if (GUI.Button(new Rect(10, 460, 200, 60), "스킬 사용"))
            {
                animator.SetTrigger("Dead");
            }
        }

    }
}
