using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
using RPG.Move;
using RPG.Fight;
using RPG.Control;

namespace RPG.AI
{
    public class AIController : BehaviourTree
    {
        Stats stats;
        Movement movement;
        Attack attack;

        private void Awake()
        {
            stats = GetComponent<Stats>();
            movement = GetComponent<Movement>();
            attack = GetComponent<Attack>();


        }

        private void Start()
        {
            var sNode = new SequenceComposite();
            var disNode = new DistanceAction(stats.attackRange, this.transform,
                        BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(this.transform).transform);
            sNode.GetChilds().Add(disNode);
            rootNode = sNode;
        }
    }
}