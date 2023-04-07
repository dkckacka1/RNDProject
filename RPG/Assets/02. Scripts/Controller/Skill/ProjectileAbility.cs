using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Skill
{
    public class ProjectileAbility : Ability
    {
        // TODO : ������ �����ؾ��ϰ� ���� ������ ������ Ȯ���ؾ���
        public int damage;
        [SerializeField] float speed;

        private void OnEnable()
        {
            StartCoroutine(ReleaseTimer());
        }

        public override void InitAbility(Transform startPos)
        {
            base.InitAbility(startPos);
            this.transform.rotation = startPos.rotation;
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