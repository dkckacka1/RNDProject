using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Fight;
using RPG.Character;

namespace RPG.Control
{

    public class EnemyController : Controller
    {
        private void Start()
        {
            BattleManager.GetInstance().liveEnemys.Add(this);
        }
    }
}