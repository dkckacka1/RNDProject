using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

namespace RPG.AI
{
    [CreateAssetMenu(fileName ="NewBT",menuName ="CreateBT",order = int.MinValue)]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode;
        public NodeStats rootStats = NodeStats.UPDATE;

        public Context context;

        public void InitNode(Context context)
        {
            this.context = context;
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
    }

}