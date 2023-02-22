using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Pattern.ObjectPooling
{
    public class Drone : MonoBehaviour
    {
        public IObjectPool<Drone> Pool { get; set; }

        public float _currentHealth;

        [SerializeField] float maxHealth = 100.0f;
        [SerializeField] float timeToSelfDestruct = 3.0f;

        // Start is called before the first frame update
        void Start()
        {
            _currentHealth = maxHealth;
        }

        private void OnEnable()
        {
            AttackPlayer();
            StartCoroutine(SelfDestruct());
        }

        private void OnDisable()
        {
            ResetDrone();
        }

        private void ResetDrone()
        {
            _currentHealth = maxHealth;
        }

        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(timeToSelfDestruct);
            TakeDamange(maxHealth);
        }

        private void ReturnToPool()
        {
            Pool.Release(this);
        }

        private void TakeDamange(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0.0f)
            {
                ReturnToPool();
            }
        }

        private void AttackPlayer()
        {
            print("Attack Player");
        }
    }

}
