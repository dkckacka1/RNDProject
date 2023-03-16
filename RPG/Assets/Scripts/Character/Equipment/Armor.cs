using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "CreateScriptableObject/CreateArmor", order = 2)]
public class Armor : Equipment
{
    public int defencePoint;
    public int hpPoint;
    [Range(0f, 0.5f)] public float movementSpeed;
    [Range(0f, 0.2f)] public float evasionPoint;
}
