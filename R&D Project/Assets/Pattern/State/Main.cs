using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.State
{
    public class Main : MonoBehaviour
    {
        private Controller _controller;

        private void Start()
        {
            _controller = (Controller)FindObjectOfType(typeof(Controller));
        }

        private void OnGUI()
        {
            if (GUILayout.Button("TurnLeft"))
                _controller.StartController();

            if (GUILayout.Button("TurnLeft"))
                _controller.Turn( Direction.LEFT);

            if (GUILayout.Button("TurnRight"))
                _controller.Turn(Direction.RIGHT);

            if (GUILayout.Button("Stop Bike"))
                _controller.StopController();
        }
    }
}
