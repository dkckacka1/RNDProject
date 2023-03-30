using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;

namespace RPG.Core
{
    public class TestScene1 : MonoBehaviour
    {
        public StageChoiceWindowUI ui;

        private void Start()
        {
            ui.Init(GameManager.Instance.UserInfo);
        }
    } 
}
