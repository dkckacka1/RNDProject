using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.SingletonClass
{
    enum SingletonType
    {
        AudioSingleton,
        GameManagerSingleton,

    }

    class SingletonManager : MonoBehaviour
    {
        private static SingletonManager instance;
        private Dictionary<SingletonType ,Singleton> singletonDic; // 싱글톤 클래스를 관리하는 Dic;
        
        public static SingletonManager Instance { get => instance; }
        

        private void Awake()
        {
            if (Instance is null)
            // 싱글톤매니저가 인스턴스화 되지 않았을 경우
            {
                instance = new SingletonManager();
                singletonDic = new Dictionary<SingletonType, Singleton>();
            }
            else
            // 다른 싱글톤매니저가 존재할 경우
            {
                DestroyImmediate(this.gameObject);
                // Destory(gameobject)는 다른 게임 오브젝트의 Update가 종료된 후에 삭제된다.
                // DestroyImmediate(gameobject)는 즉시 삭제된다. 
                return;
            }

            instance = this;
            CreateSingletonClass();
            DontDestroyOnLoad(this.gameObject);
        }

        private void CreateSingletonClass()
        {
            CreateSingleton<AudioSingleton>();
            var audioS = (AudioSingleton)singletonDic[SingletonType.AudioSingleton];
        }

        public T GetSingleton<T>() where T : Singleton
        {
            Singleton singleton;
            string str = typeof(T).Name;
            SingletonType type = (SingletonType)Enum.Parse(typeof(SingletonType), str);

            if (!singletonDic.TryGetValue(type, out singleton))
            // 찾는 싱글톤이 있다면
            {
                Debug.Log(type + "싱글톤이 존재하지 않습니다!");
                return null;
            }

            Debug.Log(singleton.GetType().Name + "싱글톤을 찾았습니다!");
            return (T)singleton;
        }

        /// <summary>
        /// ///싱글톤을 생성하는 함수(T는 싱글톤이며 new를 통한 인스턴스화를 할 수 있어야한다.)
        /// </summary>
        /// <typeparam name="T">싱글톤 이면서 new를 통한 인스턴스화가 가능해야한다.</typeparam>
        private void CreateSingleton<T>() where T : Singleton, new()
        {
            string str = typeof(T).Name;
            SingletonType t = (SingletonType)Enum.Parse(typeof(SingletonType), str);

            if (singletonDic.ContainsKey(t))
            {
                Debug.Log("해당 싱글톤이 있습니다!");
                return;
            }

            Debug.Log(t + "싱글톤을 생성합니다!");
            T singleton = new T();
            singletonDic.Add(t, singleton);
        }
    }
}
