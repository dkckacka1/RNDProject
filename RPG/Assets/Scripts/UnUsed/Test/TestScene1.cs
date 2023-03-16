using RPG.Battle.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene1 : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public PlayerStatus status;

    private void Start()
    {
        status.EquipmentWeapon(WeaponPrefab.GetComponent<Weapon>());
    }
}
