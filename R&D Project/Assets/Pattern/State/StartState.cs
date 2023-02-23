using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.State
{
    class StartState : MonoBehaviour, IState
    {
        private Controller _controller;

        public void Handle(Controller controller)
        {
            if (_controller == null)
            {
                _controller = controller;
            }

            _controller.CurrentSpeed = _controller.maxSpeed;
        }

        private void Update()
        {
            if (_controller)
            {
                if (_controller.CurrentSpeed > 0)
                {
                    _controller.transform.Translate(
                        Vector3.forward * (_controller.CurrentSpeed * Time.deltaTime));
                }
            }
        }
    }
}
