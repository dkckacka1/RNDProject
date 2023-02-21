using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

namespace RPG.AI
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        public Node rootNode;
        public NodeStats rootStats = NodeStats.UPDATE;

        public Context context;

        public void InitNode()
        {
            context = new Context(this.gameObject);
            rootStats = NodeStats.UPDATE;
            SetRootNode();
            if (rootNode != null)
            {
                rootNode.Init(this.context);
            }
        }

        public void Play()
        {
            if (rootNode != null && rootStats == NodeStats.UPDATE)
            {
                rootStats = rootNode.Update();
            }
        }

        public abstract void SetRootNode();
    }

}