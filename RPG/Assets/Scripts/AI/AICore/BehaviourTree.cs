using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AI
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        public Node rootNode;
        public NodeStats stats = NodeStats.UPDATE;

        public void Update()
        {
            if (rootNode != null && stats == NodeStats.UPDATE)
            {
                stats = rootNode.Update();
            }
        }
    }

}