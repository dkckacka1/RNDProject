using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Incant : ScriptableObject
{
    // 어느장비에 붙을 수 있는가?
    public EquipmentItemType itemType;
    // 접두인가? 접미인가?
    public IncantType incantType;
}
