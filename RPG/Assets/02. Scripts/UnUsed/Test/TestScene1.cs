using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Core;
using RPG.Battle.Core;

public class TestScene1 : MonoBehaviour
{
    public EquipmentWindowUI ui;
    Weapon weapon;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            WeaponData data = GameManager.Instance.weaponDataDic[100];
            weapon = new Weapon(data);
            ui.ShowEquipmentItem(weapon);
        }
    }

    public void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,200,60),"접두 인챈트 하기"))
        {
            weapon.Incant(1);
            ui.ShowEquipmentItem(weapon);
        }

        if (GUI.Button(new Rect(10, 80, 200, 60), "접미 인챈트 하기"))
        {
            weapon.Incant(2);
            ui.ShowEquipmentItem(weapon);

        }
    }
}
