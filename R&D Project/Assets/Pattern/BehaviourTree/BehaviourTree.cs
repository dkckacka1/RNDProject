using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Pattern.BehaviourTree;

namespace Assets.Pattern.BehaviourTree
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        [SerializeReference]
        public Node rootNode;
        public Node.State treeState = Node.State.RUNNING;

        public Node.State Update()
        {
            if (rootNode.state == Node.State.RUNNING)
            {
                treeState = rootNode.Update();
            }

            return treeState;

        }
    }
}
