using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using RPG.Control;

namespace RPG
{
    class CharacterObjectPool : MonoBehaviour
    {
        public int maxPoolSize = 10;
        public int stackDefaultCapacity = 10;
        public IObjectPool<Controller> ICharacterPool 
        { 
            get
            {
                if(_ICharacterPool == null)
                {
                    _ICharacterPool = new ObjectPool<Controller>
                        (
                        CreateFunc,
                        TakeFromPool,
                        ReturnToPool,
                        DestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize
                        );
                }

                return _ICharacterPool;
            }
        }


        private IObjectPool<Controller> _ICharacterPool;

        // 컨트롤러 생성시
        private Controller CreateFunc()
        {
            return null;
        }

        // 풀에서 꺼낼때 (OFF -> ON)
        private void TakeFromPool(Controller obj)
        {

        }

        // 풀에 넣을때 (ON -> OFF)
        private void ReturnToPool(Controller obj)
        {

        }

        // 풀이 꽉 차서 삭제 할때
        private void DestroyPoolObject(Controller obj)
        {

        }
    }
}
