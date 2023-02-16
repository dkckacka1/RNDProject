using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Simple_
{
    abstract class MonsterFactory<T> : MonoBehaviour where T : Enum
    {
        public abstract Monster CreateMonster(T type);
    }
}
