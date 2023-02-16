using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Pattern.EventBus
{
    class Main : MonoBehaviour
    {
        UnityAction startAction;
        UnityAction overAction;
        UnityAction clearAction;
        UnityAction stopAction;
        private void Awake()
        {
            this.startAction += GameStartTest;
            this.overAction += GameOverTest;
            this.clearAction += GameClearTest;
            this.stopAction += GameStopTest;
        }

        private void Start()
        {
            EventBus.SubscribeEvent(GameStateType.GAMESTART, startAction);
            EventBus.SubscribeEvent(GameStateType.GAMESTART, () => Debug.Log("스타트!"));
            EventBus.SubscribeEvent(GameStateType.GAMESTART, GameStartTest);
            // UntiyAction, 함수, 람다 적용이 가능하다.

            EventBus.SubscribeEvent(GameStateType.GAMEOVER, overAction);        
            EventBus.SubscribeEvent(GameStateType.GAMECLEAR, clearAction);
            EventBus.SubscribeEvent(GameStateType.GAMESTOP, stopAction);
            EventBus.UnsubscribeEvent(GameStateType.GAMESTOP, stopAction);
            EventBus.SubscribeEvent(GameStateType.GAMESTOP, GameStopTest);
            EventBus.UnsubscribeEvent(GameStateType.GAMESTOP, GameStopTest);
        }

        public void GameStart()
        {
            EventBus.Publish(GameStateType.GAMESTART);
        }

        public void GameOver()
        {
            EventBus.Publish(GameStateType.GAMEOVER);
        }

        public void GameClear()
        {
            EventBus.Publish(GameStateType.GAMECLEAR);
        }

        public void GameStop()
        {
            EventBus.Publish(GameStateType.GAMESTOP);
        }

        void GameStartTest()
        {
            Debug.Log("Start");
        }

        void GameOverTest()
        {
            Debug.Log("GameOver");
        }

        void GameStopTest()
        {
            Debug.Log("Stop");
        }

        void GameClearTest()
        {
            Debug.Log("Clear");
        }
    }
}
