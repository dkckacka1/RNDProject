using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Command
{
    abstract class Command
    {
        // 커맨드를 상속 받는 클래스는 Excute<T> 함수를 재정의 해야한다
        // T는 MonoBehaviour를 상속받아야 한다.
        public abstract void Excute<T>(T obj) where T : MonoBehaviour;
    }

    class MoveCommand : Command
    {
        public override void Excute<T>(T obj) 
        {
            Debug.Log("이동!");
        }
    }

    class JumpCommand : Command
    {
        public override void Excute<T>(T obj)
        {
            Debug.Log("점프!");
        }
    }

    class FireCommand : Command
    {
        public override void Excute<T>(T obj)
        {
            Debug.Log("공격!");
        }
    }
}
