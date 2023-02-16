using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Simple_
{
    class Orc : Monster
    {
        [SerializeField] orcType type;

        public override string ToString()
        {
            return (type.ToString() + name);
        }
    }
}
