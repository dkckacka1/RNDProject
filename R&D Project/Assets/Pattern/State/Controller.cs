using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.State
{
    public class Controller : MonoBehaviour
    {
        public float maxSpeed = 2.0f;
        public float turnDistance = 2.0f;

        private float _currentSpeed = 0;
        public float CurrentSpeed
        {
            get
            {
                return _currentSpeed;
            }
            set
            {
                Debug.Log("바뀜");
                _currentSpeed = value;
            }
        }

        public Direction CurrentTurnDirection
        {
            get; private set;
        }

        private IState _startState, _stopState, _turnState;

        private StateContext _stateContext;

        private void Start()
        {
            _stateContext = new StateContext(this);

            _startState = gameObject.AddComponent<StartState>();
            _stopState = gameObject.AddComponent<StopState>();
            _turnState = gameObject.AddComponent<TurnState>();

            _stateContext.Transition(_stopState);
        }

        public void StartController()
        {
            _stateContext.Transition(_startState);
        }

        public void StopController()
        {
            _stateContext.Transition(_stopState);
        }

        public void Turn(Direction direction)
        {
            CurrentTurnDirection = direction;
            _stateContext.Transition(_turnState);
        }
    }
}
