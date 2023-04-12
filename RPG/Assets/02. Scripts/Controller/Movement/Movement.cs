using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Character.Status;
using RPG.Battle.Control;
using RPG.Battle.Event;
using UnityEngine.Events;

namespace RPG.Battle.Move
{
    public class Movement
    {
        public bool canMove = true;

        BattleStatus character;
        NavMeshAgent nav;

        float attackRange;

        public bool isMove;
        public MoveEvent moveEvent;
        public IEnumerator moveEventCorotine;

        public Movement(BattleStatus character, NavMeshAgent nav)
        {
            this.character = character;
            this.nav = nav;
            moveEvent = new MoveEvent();
            moveEventCorotine = MoveEvent();
        }

        public void AddMoveEvent(UnityAction<BattleStatus> action)
        {
            moveEvent.AddListener(action);
        }

        public void UpdateStatus(Controller controller)
        {
            attackRange = controller.battleStatus.status.AttackRange;
            nav.speed = controller.battleStatus.status.MovementSpeed;
            nav.stoppingDistance = controller.battleStatus.status.AttackRange;
        }

        public void ResetNav()
        {
            if (nav.enabled == true)
                nav.ResetPath();
        }

        public void MoveNav(Transform target)
        {
            nav.SetDestination(target.position);
        }

        public void Move(Transform target)
        {
            Vector3 movementVector = new Vector3(target.position.x, 0, target.position.z);
            character.transform.LookAt(movementVector);
            //transform.Translate(Vector3.forward * status.movementSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 사정거리 외면 true, 사정거리 이내면 false
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.character.transform.position) > attackRange;
        }

        IEnumerator MoveEvent()
        {
            while (true)
            {
                if (isMove)
                {
                    moveEvent.Invoke(character);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}