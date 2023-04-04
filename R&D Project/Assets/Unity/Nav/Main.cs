using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Unity.Nav
{
    public class Main : MonoBehaviour
    {
        public NavMeshAgent nav;
        public GameObject target;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 70, 70), "추적 시작"))
            {
                nav.SetDestination(target.transform.position);
            }

            if (GUI.Button(new Rect(10, 90, 70, 70), "추적 해제"))
            {
                nav.ResetPath();
            }
        }
    }
}

