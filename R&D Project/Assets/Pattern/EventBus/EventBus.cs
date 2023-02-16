using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Pattern.EventBus
{
    public enum GameStateType
    {
        GAMESTART,
        GAMEOVER,
        GAMECLEAR,
        GAMESTOP
    };

    class EventBus : MonoBehaviour
    {
        private static readonly Dictionary<GameStateType, UnityEvent> 
            gameEventBusDic = new Dictionary<GameStateType, UnityEvent>();
        // 게임 이벤트를 담는 Dic, GameStateType을 통하여 이벤트를 호출한다.


        /// <summary>
        /// 이벤트 삽입 함수
        /// </summary>
        /// <param name="gameStateType"></param>
        /// <param name="listener"></param>
        public static void SubscribeEvent(GameStateType gameStateType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (gameEventBusDic.TryGetValue(gameStateType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                gameEventBusDic.Add(gameStateType, thisEvent);
            }
        }

        public static void UnsubscribeEvent(GameStateType gameStateType, UnityAction unityAction)
        {
            UnityEvent thisEvent;

            if (gameEventBusDic.TryGetValue(gameStateType, out thisEvent))
            {
                thisEvent.RemoveListener(unityAction);
            }
        }
        
        public static void Publish(GameStateType gameStateType)
        {
            UnityEvent thisEvent;

            if (gameEventBusDic.TryGetValue(gameStateType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}
