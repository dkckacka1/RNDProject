using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaopn", menuName = "CreateScriptableObject/CreateWeaponData", order = 1)]
public class WeaponData : EquipmentData
{
    public int weaponDamage;
    public float weaponAttackSpeed;
    public float weaponAttackDistance;
    public float movementSpeed;
    public float criticalPoint;
    // 속성 { 불 물 바람 etc... }
    // 공격 시 특수한 효과 비스무리한거
    // 인챈트 접미(화염, 불꽃, 얼음) 접두 (강렬한, 신속의)
}
