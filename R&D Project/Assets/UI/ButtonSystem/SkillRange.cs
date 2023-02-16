using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.UI.ButtonSystem
{
    class SkillRange : MonoBehaviour
    {
        List<GameObject> gameObjects = new List<GameObject>();
        public UnityAction skillAction;

        private void Awake()
        {
            skillAction += () =>
            {
                foreach (var item in gameObjects)
                {
                    print(item.name + "으악!");
                    item.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            MeshRenderer render;
            if (other.tag == "Enemy")
            {
                if ((render = other.GetComponent<MeshRenderer>()) != null)
                {
                    render.material.color = Color.red;
                }
            }
            gameObjects.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            MeshRenderer render;
            if (other.tag == "Enemy")
            {
                if ((render = other.GetComponent<MeshRenderer>()) != null)
                {
                    render.material.color = Color.blue;
                }
            }
            gameObjects.Remove(other.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 2f);
        }

    }
}
