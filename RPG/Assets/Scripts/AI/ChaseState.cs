using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Control;
using RPG.Move;

namespace RPG.AI
{
    public class ChaseState : State,IState
    {
        private Movement movement;

        public override void Handle(Controller controller)
        {
            base.Handle(controller);
            movement = controller.movement;
        }

        public void OnEnd()
        {
            controller.animator.SetBool("isMove", false);
        }

        public void OnStart()
        {
            controller.animator.SetBool("isMove", true);
        }

        public void OnUpdate()
        {
            movement.Move(controller.Target.transform);
        }
    }
}
