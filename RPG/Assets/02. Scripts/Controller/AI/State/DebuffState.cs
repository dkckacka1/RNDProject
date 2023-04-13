
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.Battle.AI
{
    public class DebuffState : State, IState
    {
        delegate void DebuffAction(Controller character, Controller target);
        DebuffAction action;

        public DebuffState(Controller controller) : base(controller)
        {
        }

        public void OnEnd()
        {
            action = null;
        }

        public void OnStart()
        {
            controller.currentAIState = AIState.Debuff;
            switch (controller.battleStatus.currentDebuff)
            {
                case DebuffType.Stern:
                    controller.animator.SetTrigger("Stern");
                    controller.movement.ResetNav();
                    action = SternAction;
                    break;
                case DebuffType.Temptation:
                    //controller.animator.SetTrigger("Move");
                    controller.movement.ResetNav();
                    action = TemptationAction;
                    break;
                case DebuffType.Fear:
                    //controller.animator.SetTrigger("Move");
                    controller.movement.ResetNav();
                    action = FearAction;
                    break;
            }
        }

        public void OnUpdate()
        {
            action.Invoke(controller, controller.target);
        }

        public void SternAction(Controller character, Controller target)
        {
            // 그자리에서 가만히
        }

        public void TemptationAction(Controller character, Controller target)
        {
            // 타겟으로 이동
        }

        public void FearAction(Controller character, Controller target)
        {
            // 타겟에서 멀어짐
        }
    }
}
