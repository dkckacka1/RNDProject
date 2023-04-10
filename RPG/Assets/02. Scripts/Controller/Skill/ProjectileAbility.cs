using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Ability
{
    public class ProjectileAbility : Ability
    {
        public int damage;
        [SerializeField] float speed;

        private void OnEnable()
        {
            StartCoroutine(ReleaseTimer());
        }

        public override void InitAbility(Transform startPos)
        {
            this.transform.rotation = startPos.rotation;
            base.InitAbility(startPos);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                var enemyStatus = enemyController.battleStatus;
                if (enemyStatus != null)
                {
                    enemyStatus.TakeDamage(damage);
                }
            }

        }
    }

}