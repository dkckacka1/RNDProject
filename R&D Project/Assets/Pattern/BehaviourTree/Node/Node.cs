using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.BehaviourTree
{
    public abstract class Node : MonoBehaviour
    {
        public enum State
        {
            RUNNING,
            SUCCESS,
            FAILURE
        }

        public State state;
        public bool isStart = false;

        public State Update()
        {
            if (!isStart)
            {
                OnStart();
                isStart = true;
            }

            state = OnUpdate();

            if (state == State.FAILURE || state == State.SUCCESS)
            {
                OnStop();
                isStart = false;
            }

            return state;
        }

        public abstract void OnStart();
        public abstract void OnStop();
        public abstract State OnUpdate();
    }

}