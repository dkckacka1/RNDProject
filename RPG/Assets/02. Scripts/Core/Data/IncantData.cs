using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData", order = 0)]
public class IncantData : Data
{
    public string className;
    public IncantType incantType;
    public EquipmentItemType itemType;
    public string incantName;
    public string addDesc;
    public string minusDesc;
    [Header("Ability")]
    public bool isIncantAbility;
    [TextArea()]
    public string abilityDesc;
    public Sprite abilityIcon;
}
