using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Skill
{
    public class ProjectileSkill : Skill
    {
        // TODO : ������ �����ؾ��ϰ� ���� ������ ������ Ȯ���ؾ���
        public int damage;
        [SerializeField] float speed;
        [SerializeField] float destroyTime;

        private void Start()
        {
            Destroy(this.gameObject, destroyTime);
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