using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ScriptableObject
{
    public string EquipmentName;
    public EquipmentType equipmentType;
    public EquipmentTier equipmentTier;
    [Space()]
    [TextArea()]
    public string description;
}
