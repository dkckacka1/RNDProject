using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Constraint : MonoBehaviour
    {
        public virtual bool WillViolate(Path path)
        {
            return true;
        }

        public virtual Goal Suggest(Path path)
        {
            return new Goal();
        }
    }
}