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

namespace RPG.UnUsed
{
    public class TestScene1 : MonoBehaviour
    {
        public GetItemText txt;

        public int gain;
        public float time;


        private void Start()
        {
            txt.GainText(gain, time, () => { Debug.Log("Complete"); });
        }
    }
}
