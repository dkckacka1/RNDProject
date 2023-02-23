using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.State
{
    class StopState : MonoBehaviour, IState
    {
        private Controller _controller;
        public void Handle(Controller controller)
        {
            if (_controller == null)
            {
                _controller = controller;
            }

            _controller.CurrentSpeed = 0;   
        }
    }
}
