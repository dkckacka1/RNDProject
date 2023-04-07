using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Core; 

namespace RPG.Character.Status
{
    public class PlayerStatus : Status
    {
        public Weapon currentWeapon;
        public Armor currentArmor;
        public Helmet currentHelmet;
        public Pants currentPants;

        public void SetEquipment()
        {
            if (currentWeapon == null ||
                currentArmor == null ||
                currentHelmet == null ||
                currentPants == null)
            {
                Debug.LogError("��� �������� �����ϴ�.");    
                return;
            }

            attackDamage =          currentWeapon.attackDamage;
            attackRange =           currentWeapon.attackChance;
            attackSpeed =           currentWeapon.attackSpeed;
            criticalChance =        currentWeapon.criticalChance;
            criticalDamage =        currentWeapon.criticalDamage;
            attackChance =          currentWeapon.attackChance;

            maxHp =                 currentArmor.hpPoint + currentHelmet.hpPoint + currentPants.hpPoint;
            defencePoint =          currentArmor.defencePoint + currentHelmet.defencePoint + currentPants.defencePoint;
            evasionPoint =          currentArmor.evasionPoint;
            decreseCriticalDamage = currentHelmet.decreseCriticalDamage;
            evasionCritical =       currentHelmet.evasionCritical;

            movementSpeed =         currentWeapon.movementSpeed + currentPants.movementSpeed;
        }

        public void SetPlayerStatusFromUserinfo(UserInfo userInfo)
        {
            // 1.��� ����
            WeaponData w_data;
            ArmorData a_data;
            HelmetData h_data;
            PantsData p_data;
            GameManager.Instance.GetEquipmentData(userInfo.lastedWeapon, out w_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedArmor, out a_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedHelmet, out h_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedPants, out p_data);

            // 1-1. ��� ��ȭ ��ġ ����
            // 1-2. ��� ��æƮ ����
            // 1-3. ��� ������ ������Ʈ

            if (w_data)
            {
                Weapon weapon = new Weapon(w_data);
                weapon.reinforceCount = userInfo.weaponReinforceCount;

                if (userInfo.weaponPreifxIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.weaponPreifxIncantID];
                    weapon.Incant(prefixIncant);
                }

                if (userInfo.weaponSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.weaponSuffixIncantID];
                    weapon.Incant(suffixIncant);
                }
                weapon.UpdateItem();

                this.EquipItem(weapon);
            }
            else
                Debug.LogError("Weapon is null");

            if (a_data)
            {
                Armor armor = new Armor(a_data);
                armor.reinforceCount = userInfo.armorReinforceCount;

                if (userInfo.armorPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.armorPrefixIncantID];
                    armor.Incant(prefixIncant);
                }

                if (userInfo.armorSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.armorSuffixIncantID];
                    armor.Incant(suffixIncant);
                }
                armor.UpdateItem();

                this.EquipItem(armor);
            }
            else
                Debug.LogError("Armor is null");


            if (h_data)
            {
                Helmet helmet = new Helmet(h_data);
                helmet.reinforceCount = userInfo.helmetReinforceCount;
                if (userInfo.helmetPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.helmetPrefixIncantID];
                    helmet.Incant(prefixIncant);
                }

                if (userInfo.helmetSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.helmetSuffixIncantID];
                    helmet.Incant(suffixIncant);
                }
                helmet.UpdateItem();

                this.EquipItem(helmet);
            }
            else
                Debug.LogError("Helmet is null");


            if (p_data)
            {
                Pants pants = new Pants(p_data);
                pants.reinforceCount = userInfo.pantsReinforceCount;

                if (userInfo.pantsPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.pantsPrefixIncantID];
                    pants.Incant(prefixIncant);
                }

                if (userInfo.pantsSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.pantsPrefixIncantID];
                    pants.Incant(suffixIncant);
                }

                pants.UpdateItem();

                this.EquipItem(pants);
            }
            else
                Debug.LogError("Pants is null");


            // 2.��� ���� �������ͽ� ��ȭ���ֱ�
            this.SetEquipment();
        }

        public void SetPlayerStatusFromStatus(PlayerStatus status, CharacterAppearance ap = null)
        {
            currentWeapon = new Weapon(status.currentWeapon);
            currentArmor = new Armor(status.currentArmor);
            currentHelmet = new Helmet(status.currentHelmet);
            currentPants = new Pants(status.currentPants);


            if (ap != null)
            {
                ap.EquipWeapon(currentWeapon.weaponLook);
            }

            this.SetEquipment();
        }

        #region ���_����
        public void EquipItem(Weapon weapon)
        {
            currentWeapon = weapon;
        }

        public void EquipItem(Armor armor)
        {
            currentArmor = armor;
        }

        public void EquipItem(Helmet helmet)
        {
            currentHelmet = helmet;
        }

        public void EquipItem(Pants pants)
        {
            currentPants = pants;
        }
        #endregion
    }
}