using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Move
{
    public class Movement : MonoBehaviour
    {
        public bool canMove = true;

        Status stats;

        private void Awake()
        {
            stats = GetComponent<Status>();
        }

        public void Move(Transform target)
        {
            Vector3 movementVector = new Vector3(target.position.x, 0, target.position.z);
            this.transform.LookAt(movementVector);
            transform.Translate(Vector3.forward * stats.moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 사정거리 외면 true, 사정거리 이내면 false
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.transform.position) > stats.attackRange;
        }
    }
}