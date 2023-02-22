using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Pattern.ObjectPooling
{
    public class DroneObjectPool : MonoBehaviour
    {
        public int maxPoolSize = 10;
        public int stackDefaultCapacity = 10;

        public IObjectPool<Drone> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<Drone>(
                        CreatePooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize
                        );
                }

                return _pool;
            }
        }

        private void OnDestroyPoolObject(Drone obj)
        {
            Destroy(obj.gameObject);
        }

        private void OnReturnedToPool(Drone obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Drone obj)
        {
            obj.gameObject.SetActive(true);
        }

        private Drone CreatePooledItem()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Drone drone = go.AddComponent<Drone>();

            go.name = "Drone";
            drone.Pool = Pool;

            return drone;
        }

        private IObjectPool<Drone> _pool;

        public void Spawn()
        {
            var amount = UnityEngine.Random.Range(1, 10);

            for (int i = 0; i < amount; i++)
            {
                var drone = Pool.Get();

                drone.transform.position = UnityEngine.Random.insideUnitSphere * 10;
            }
        }
    }
}