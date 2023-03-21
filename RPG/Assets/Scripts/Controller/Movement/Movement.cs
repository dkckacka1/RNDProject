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

        public Movement(Transform transform, Status status, NavMeshAgent nav)
        {
            this.transform = transform;
            this.nav = nav;
            this.status = status;

            nav.speed = status.movementSpeed;
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
        /// �����Ÿ� �ܸ� true, �����Ÿ� �̳��� false
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.transform.position) > status.attackRange;
        }
    }
}