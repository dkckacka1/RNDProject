using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public class PrintAction : Action
    {
        public override bool Run()
        {
            Debug.Log("TestActionRun");
            return true;
        }
    }
}