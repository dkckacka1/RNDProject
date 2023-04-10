using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPants", menuName = "CreateScriptableObject/IncantData", order = 0)]
public class IncantData : Data
{
    public string className;
    public EquipmentItemType itemType;
}
