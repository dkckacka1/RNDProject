using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.State
{
    class TurnState : MonoBehaviour, IState
    {
        private Vector3 _turnDirection = new Vector3();
        private Controller _controller;
        public void Handle(Controller controller)
        {
            if (_controller == null)
            {
                _controller = controller;
            }

            _turnDirection.x =
                (float)_controller.CurrentTurnDirection;

            if (_controller.CurrentSpeed > 0)
            {
                transform.Translate(_turnDirection * controller.turnDistance);
            }
        }
    }
}
