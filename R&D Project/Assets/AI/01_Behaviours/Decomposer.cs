using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Decomposer : MonoBehaviour
    {
        public virtual Goal Decompose(Goal goal)
        {
            return goal;
        }
    }

}