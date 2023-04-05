using System.Collections;
using System.Collections.Generic;
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
        public bool canAttack = true;
        public float attackSpeed = 1;
        public float attackDelay = 0;
        public float test1;
        public float test2;
        public Slider slider;
        public float attackTImechecker = 1.5f;

        private void Start()
        {
            RuntimeAnimatorController rac = animator.runtimeAnimatorController;

            foreach (var anim in rac.animationClips)
            {
                if (anim.name == "MeleeAttack_OneHanded")
                {
                    Debug.Log(anim.name + " : " + anim.length);
                    attackDelay = anim.length;
                    break;
                }
            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "공격하기"))
            {
                animator.SetFloat("AttackSpeed", this.attackSpeed);
                if (canAttack)
                {

                    animator.SetTrigger("Attack");
                    canAttack = false;
                    StartCoroutine(AttackTimeCheck());
                    StartCoroutine(AttackDelayCheck());
                }
            }

            if (GUI.Button(new Rect(10, 70, 200, 60), ""))
            {
            }

            if (GUI.Button(new Rect(10, 130, 200, 60), ""))
            {
            }
        }

        IEnumerator AttackTimeCheck()
        {
            test1 = attackDelay / attackSpeed; 
            yield return new WaitForSeconds(attackDelay / attackSpeed);
            canAttack = true;
        }

        IEnumerator AttackDelayCheck()
        {
            test2 = attackDelay / attackSpeed / attackTImechecker;
            yield return new WaitForSeconds(attackDelay / attackSpeed / attackTImechecker);
            slider.value -= 10;
        }

    }
}
