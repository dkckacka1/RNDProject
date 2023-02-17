using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public class DebugAction : ActionNode
    {
        public string log;

        public DebugAction(string log)
        {
            this.log = log;
        }

        public override void OnStart()
        {
            Debug.Log($"OnStart : {log}");
        }

        public override void OnStop()
        {
            Debug.Log($"OnStop : {log}");
        }

        public override State OnUpdate()
        {
            Debug.Log($"OnUpdate : {log}");
            return State.SUCCESS;
        }
    }

}