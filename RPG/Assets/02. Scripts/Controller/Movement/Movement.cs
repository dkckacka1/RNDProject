using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Character.Status;
using RPG.Battle.Control;

namespace RPG.Battle.Move
{
    public class Movement
    {
        public bool canMove = true;

        Transform transform;
        NavMeshAgent nav;

        float attackRange;

        public Movement(Transform transform, NavMeshAgent nav)
        {
            this.transform = transform;
            this.nav = nav;
        }

        public void UpdateStatus(Controller controller)
        {
            attackRange = controller.battleStatus.status.attackRange;
            nav.speed = controller.battleStatus.status.movementSpeed;
            nav.stoppingDistance = controller.battleStatus.status.attackRange;
        }

        public void ResetNav()
        {
            nav.ResetPath();
        }

        public void MoveNav(Transform target)
        {
            nav.SetDestination(target.position);
        }

        public void Move(Transform target)
        {
            Vector3 movementVector = new Vector3(target.position.x, 0, target.position.z);
            transform.LookAt(movementVector);
            //transform.Translate(Vector3.forward * status.movementSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 사정거리 외면 true, 사정거리 이내면 false
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.transform.position) > attackRange;
        }
    }
}