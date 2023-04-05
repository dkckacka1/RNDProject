using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public LootingItem loot;
        public Canvas canvas;
        public Transform backpack;

        public GameObject sh;
        private void Start()
        {
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "ø°≥ ¡ˆ ∂≥±∏±‚"))
            {
                sh.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                LootingItem loots = Instantiate(loot,canvas.transform);
                loots.Init(Camera.main.WorldToScreenPoint(sh.transform.position), DropItemType.Energy);
            }

            if (GUI.Button(new Rect(10, 70, 200, 60), "∞≠»≠±« ∂≥±∏±‚"))
            {
                sh.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                LootingItem loots = Instantiate(loot, Camera.main.WorldToScreenPoint(sh.transform.position), Quaternion.identity, canvas.transform);
                loots.Init(Camera.main.WorldToScreenPoint(sh.transform.position), DropItemType.reinfoceScroll);
            }

            if (GUI.Button(new Rect(10, 130, 200, 60), "ªÃ±‚±« ∂≥±∏±‚"))
            {
                sh.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                LootingItem loots = Instantiate(loot, Camera.main.WorldToScreenPoint(sh.transform.position), Quaternion.identity, canvas.transform);
                loots.Init(Camera.main.WorldToScreenPoint(sh.transform.position), DropItemType.GachaItemScroll);
            }

            if (GUI.Button(new Rect(10, 190, 200, 60), "¿Œ√¶∆Æ±« ∂≥±∏±‚"))
            {
                sh.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                LootingItem loots = Instantiate(loot, Camera.main.WorldToScreenPoint(sh.transform.position), Quaternion.identity, canvas.transform);
                loots.Init(Camera.main.WorldToScreenPoint(sh.transform.position), DropItemType.IncantScroll);
            }
        }

    }
}
