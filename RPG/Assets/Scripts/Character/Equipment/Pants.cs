using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPants", menuName = "CreateScriptableObject/CreatePants", order = 4)]
public class Pants : Equipment
{
    public int defencePoint;
    public int hpPoint;
    [Range(0f, 0.5f)] public float movementSpeed;
}
