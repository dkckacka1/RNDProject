using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Control;
using RPG.Battle.Move;

namespace RPG.Battle.AI
{
    public class ChaseState : State,IState
    {
        private Movement movement;

        public ChaseState(Controller controller) : base(controller)
        {
            movement = controller.movement;
        }

        public void OnEnd()
        {
            controller.animator.SetBool("isMove", false);
        }

        public void OnStart()
        {
            controller.animator.SetBool("isMove", true);
            movement.SetNav();
            controller.state = CombatState.Chase;
        }

        public void OnUpdate()
        {
            movement.MoveNav(controller.target.transform);
            //movement.Move(controller.Target.transform);
        }
    }
}
