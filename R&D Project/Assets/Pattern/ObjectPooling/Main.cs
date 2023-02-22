using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.ObjectPooling
{
    public class Main : MonoBehaviour
    {
        private DroneObjectPool _pool;

        private void Start()
        {
            _pool = gameObject.AddComponent<DroneObjectPool>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Drone"))
            {
                _pool.Spawn();
            }
        }
    }
}
