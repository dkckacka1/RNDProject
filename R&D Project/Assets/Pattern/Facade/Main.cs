using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.Facade
{
    public class Main : MonoBehaviour
    {
        public Facade facade;

        private void Awake()
        {
            facade = GetComponent<Facade>();
        }

        public void TurnOn()
        {
            print($"메인에서는 함수하나만 호출하면 된다.");
            facade.TurnOnAction();
        }

        public void TurnOff()
        {
            print($"메인에서는 함수하나만 호출하면 된다.");
            facade.TurnOffAction();
        }
    }
}
