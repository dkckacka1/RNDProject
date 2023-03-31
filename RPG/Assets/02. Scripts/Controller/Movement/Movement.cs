using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Character.Status;

namespace RPG.Battle.Move
{
    public class Movement
    {
        public bool canMove = true;

        Transform transform;
        NavMeshAgent nav;
        Status status;

        public Movement(Transform transform, BattleStatus battleStatus, NavMeshAgent nav)
        {
            this.transform = transform;
            this.nav = nav;
            this.status = battleStatus.status;
        }

        public void SetNav()
        {
            nav.speed = status.movementSpeed;
            nav.stoppingDistance = status.attackRange;
        }

        public void MoveNav(Transform target)
        {
            nav.SetDestination(target.position);
        }

        public void Move(Transform target)
        {
            Vector3 movementVector = new Vector3(target.position.x, 0, target.position.z);
            transform.LookAt(movementVector);
            transform.Translate(Vector3.forward * status.movementSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 사정거리 외면 true, 사정거리 이내면 false
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.transform.position) > status.attackRange;
        }
    }
}