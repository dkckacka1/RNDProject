using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Core;


public class TestScene1 : MonoBehaviour
{
    public PlayerStatus status;

    private void Start()
    {
        {
            var armor = ResoucesLoader.LoadEquipment<ArmorData>("Armor", EquipmentItemType.Armor);
            print(armor.description);
            status.currentArmor = new Armor(armor);
            var weapon = ResoucesLoader.LoadEquipment<WeaponData>("Weapon", EquipmentItemType.Weapon);
            status.currentWeapon = new Weapon(weapon);
            var helmet = ResoucesLoader.LoadEquipment<HelmetData>("Helmet", EquipmentItemType.Helmet);
            status.currentHelmet = new Helmet(helmet);
            var pants = ResoucesLoader.LoadEquipment<PantsData>("Pants", EquipmentItemType.Pants);
            status.currentPants = new Pants(pants);

            status.Initialize();
        }

        //status.EquipmentWeapon(WeaponPrefab.GetComponent<Weapon>());

    }
}
