using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Unity.Scriptable2
{
    public interface ITest
    {

    }

    [System.Serializable]
    public class TestScript
    {
        public int testValue1;
    }

    public class derivedClass1 : TestScript, ITest
    {
        public string testValue2;
    }

    public class derivedClass2 : TestScript, ITest
    {
        public float testValue2;
    }

    [CreateAssetMenu(fileName = "newScriptableObj", menuName = "ScriptableObj/TestObj", order = 0)]
    public class ScriptableObject : UnityEngine.ScriptableObject
    {
        [SerializeReference]
        public List<TestScript> testList = new List<TestScript>() 
        {
            new derivedClass1(),
            new derivedClass2()
        };
    }
}