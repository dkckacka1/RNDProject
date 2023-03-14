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
    // �Ӽ� { �� �� �ٶ� etc... }
    // ���� �� Ư���� ȿ�� �񽺹����Ѱ�
    // ��æƮ ����(ȭ��, �Ҳ�, ����) ���� (������, �ż���)
}
