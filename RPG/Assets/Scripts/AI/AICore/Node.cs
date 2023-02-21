using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

namespace RPG.AI
{
    public abstract class Node
    {
        public Context context;

        public NodeStats stats;
        bool isStart = false;

        public NodeStats Update()
        {
            if (!isStart)
            {
                isStart = true;
                OnStart();
            }

            stats = OnUpdate();

            if (stats == NodeStats.SUCCESS || stats == NodeStats.FAILURE)
            {
                isStart = false;
                OnStop();
            }

            return stats;
        }

        public abstract void Init(Context context);
        public abstract void OnStart();
        public abstract void OnStop();
        public abstract NodeStats OnUpdate();
    }
}