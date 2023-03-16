using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public string EquipmentName;
    public EquipmentType equipmentType;
    public EquipmentTier equipmentTier;
    [TextArea()]
    public string description;
}
