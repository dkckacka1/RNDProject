using RPG.Battle.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Battle.Move
{
    public class Movement : MonoBehaviour
    {
        public bool canMove = true;

        NavMeshAgent nav;
        Status status;

        private void Awake()
        {
            status = GetComponent<Status>();
            nav = GetComponent<NavMeshAgent>();

            nav.speed = status.moveSpeed;
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
            transform.Translate(Vector3.forward * status.moveSpeed * Time.deltaTime);
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