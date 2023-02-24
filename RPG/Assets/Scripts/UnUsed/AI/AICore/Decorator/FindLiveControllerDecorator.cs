using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.UnUsed
{
    /// <summary>
    /// 살아있는 컨트롤러를 찾아줍니다.
    /// 찾으면 SUCCESS, 못찾으면 FAILURE
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FindLiveControllerDecorator<T> : DecoratorNode where T : Controller
    {
        T target;

        public override void OnStart()
        {
            target = BattleManager.GetInstance().ReturnNearDistanceController<T>(context.transform);
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            context.controller.Target = target;
            if (context.controller.Target == null)
            {
                return Stats.FAILURE;
            }
            else
            {
                return Stats.SUCCESS;
            }
        }
    }
}
