using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Pattern.BehaviourTree;

namespace Assets.Pattern.BehaviourTree
{
    [CreateAssetMenu(fileName ="CreateBT",menuName ="CreateBT",order = int.MaxValue)]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeReference] public Composite com;
        //public Selector startSelector = new Selector();

        //private void Awake()
        //{
        //    startSelector.AddChild(new PrintAction());
        //}

        //private void Start()
        //{
        //    Run();
        //}

        //public void Run()
        //{
        //    if (startSelector.Run())
        //    {
        //        print("성공");
        //    }
        //    else
        //    {
        //        print("실패");
        //    }
        //}
    }
}
