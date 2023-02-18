using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.Facade
{
    public class Facade : MonoBehaviour
    {
        public void TurnOnAction()
        {
            print("수많은 작업 그리고 TurnOn");
        }

        public void TurnOffAction()
        {
            print("수많은 작업 그리고 TurnOff");
        }

    }
}
