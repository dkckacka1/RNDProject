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
    Equipment item;
    Weapon weapon;
    Armor armor;
    Helmet helmet;
    Pants pants;


    private void Start()
    {
        if(GameManager.Instance != null)
        {
            WeaponData weaponData = GameManager.Instance.weaponDataDic[100];
            ArmorData armorData = GameManager.Instance.armorDataDic[200];
            HelmetData helmetData = GameManager.Instance.helmetDataDic[300];
            PantsData pantsData = GameManager.Instance.pantsDataDic[400];
            weapon = new Weapon(weaponData);
            armor = new Armor(armorData);
            helmet = new Helmet(helmetData);
            pants = new Pants(pantsData);
            item = weapon;
            ui.ShowEquipmentItem(weapon);
        }
    }

    public void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,200,60),"���� ��æƮ �ϱ�"))
        {
            Incant incant;
            if (GameManager.Instance.GachaIncat(item.equipmentType, IncantType.prefix, out incant))
            {
                item.Incant(incant);
            }
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 80, 200, 60), "���� ��æƮ �ϱ�"))
        {
            Incant incant;
            if (GameManager.Instance.GachaIncat(item.equipmentType, IncantType.suffix, out incant))
            {
                item.Incant(incant);
            }
            ui.ShowEquipmentItem(item);

        }

        if (GUI.Button(new Rect(10, 150, 200, 60), "���� ����"))
        {
            item = weapon;
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 220, 200, 60), "�Ƹ� ����"))
        {
            item = armor;
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 290, 200, 60), "���� ����"))
        {
            item = helmet;
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 360, 200, 60), "���� ����"))
        {
            item = pants;
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 430, 200, 60), "��� ��æƮ �����"))
        {
            item.RemoveAllIncant();
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 500, 200, 60), "��ȭ �ϱ�"))
        {
            ui.ShowEquipmentItem(item);
        }

        if (GUI.Button(new Rect(10, 570, 200, 60), "��� ��ȭ ��ġ �����"))
        {
            ui.ShowEquipmentItem(item);
        }
    }
}
