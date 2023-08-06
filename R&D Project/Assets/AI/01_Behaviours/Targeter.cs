using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Behaviour
{
    public class Targeter : MonoBehaviour
    {
        public virtual Goal GetGoal()
        {
            return new Goal();
        }
    }

}