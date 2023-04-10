using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public Image image;

        private void Start()
        {

        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "스킬 사용"))
            {
                var list = GameManager.Instance.incantDic.Where(incant => incant.Value.isIncantAbility == true).ToList();


                var random = Random.Range(0, list.Count);
                var sprite = list[random].Value.abilityIcon;

                Debug.Log(sprite);
                
                image.sprite = sprite;
            }
        }

    }
}
