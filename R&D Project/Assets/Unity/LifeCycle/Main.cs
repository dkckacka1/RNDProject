using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Unity.LifeCycle
{
    public class Main : MonoBehaviour
    {
        public GameObject prefab;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 60, 60), "Test"))
            {
                Instantiate(prefab);
            }
        }
    }
}
