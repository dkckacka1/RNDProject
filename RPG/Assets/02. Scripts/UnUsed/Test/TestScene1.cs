using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;
using RPG.Battle.UI;
using RPG.Battle.Control;

namespace RPG.Core
{
    public class TestScene1 : MonoBehaviour
    {
        public ObjectPooling pooling;
        public List<EnemyController> controllers;
        public EnemyData data;
        public float timer;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "Ç®¿¡¼­ »©±â"))
            {
                float randomfloat1 = Random.Range(-5f, 5f);
                float randomfloat2 = Random.Range(-5f, 5f);
                controllers.Add(pooling.GetEnemyController(data, new Vector3(randomfloat1, 0, randomfloat2)));
            }

            if (GUI.Button(new Rect(10, 70, 200, 60), "Ç®¿¡ ³Ö±â"))
            {
                if (controllers.Count <= 0)
                {
                    return;
                }
                var controller = controllers[Random.Range(0, controllers.Count)];
                pooling.ReturnEnemy(controller);
                controllers.Remove(controller);
            }

            if (GUI.Button(new Rect(10, 130, 200, 60), "À¯´Ö Á×ÀÌ±â"))
            {
                if (controllers.Count <= 0)
                {
                    return;
                }
                var controller = controllers[Random.Range(0, controllers.Count)];
                StartCoroutine(test(controller));
            }
        }

        IEnumerator test(EnemyController enemy)
        {
            enemy.animator.SetTrigger("Dead");
            yield return new WaitForSeconds(timer);
            pooling.ReturnEnemy(enemy);
            controllers.Remove(enemy);
        }
    }
}
