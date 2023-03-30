using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class UserInfo
    {
        public int itemReinforceCount; // ��� ��ȭ��
        public int itemIncantCount; // ��� ��æƮ��
        public int itemGachaTicket; // ��� �̱��
        public int risingTopCount;
        public int Energy;

        // Weapon
        public int lastedWeapon;
        public int weaponReinforceCount;
        public int weaponPreifxIncantID;
        public int weaponSuffixIncantID;
        // Armor
        public int lastedArmor;
        public int armorReinforceCount;
        public int armorPrefixIncantID;
        public int armorSuffixIncantID;
        // Helmet
        public int lastedHelmet;
        public int helmetReinforceCount;
        public int helmetPrefixIncantID;
        public int helmetSuffixIncantID;

        // Pants
        public int lastedPants;
        public int pantsReinforceCount;
        public int pantsPrefixIncantID;
        public int pantsSuffixIncantID;
    }

}