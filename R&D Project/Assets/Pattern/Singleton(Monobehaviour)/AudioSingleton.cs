using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Singleton_Monobehaviour_
{
    class AudioSingleton : Singleton
    {

        public override string ToString()
        {
            return "오디오 싱글톤";
        }
    }
}
