using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Actuator : MonoBehaviour
    {
        public virtual Path GetPath(Goal goal)
        {
            return new Path();
        }

        public virtual Steering GetOutput(Path path, Goal goal)
        {
            return new Steering();
        }
    }
}