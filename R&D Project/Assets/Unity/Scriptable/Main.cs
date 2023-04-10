using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Unity.Scriptable
{
    public class Main : MonoBehaviour
    {
        public string str = "TestClass";

        private void Start()
        {
            Type t = Type.GetType("Assets.Unity.Scriptable." + str);
        }
    }
}
