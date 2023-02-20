using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Move;
using RPG.Fight;
using RPG.Control;

namespace RPG.Character
{
    public class Context 
    {
        public GameObject gameobject;

        public Transform transform;
        public Rigidbody rigidbody;

        public Stats stats;
        public Movement movement;
        public Attack attack;
        public Controller controller;

        public void InitContext(GameObject gameobject)
        {
            this.gameobject = gameobject;

            transform = gameobject.transform;
            rigidbody = gameobject.GetComponent<Rigidbody>();

            stats = gameobject.GetComponent<Stats>();
            movement = gameobject.GetComponent<Movement>();
            attack = gameobject.GetComponent<Attack>();
            controller = gameobject.GetComponent<Controller>();
        }
    }

}