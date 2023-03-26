using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class UserInfo
    {
        public int equipmentReinforcement; // ��� ��ȭ��
        public int equipmentIncant; // ��� ��æƮ��
        public int equipmentticket; // ��� �̱��

        // Weapon
        public int lastedWeapon;
        public int weaponReinforceCount;
        public int weaponprefixIncant;
        public int weaponSuffixIncant;
        // Armor
        public int lastedArmor;
        public int armorReinforceCount;
        public int armorprefixIncan;
        public int armorSuffixIncan;
        // Helmet
        public int lastedHelmet;
        public int helmetReinforceCount;
        public int helmetprefixIncan;
        public int helmetSuffixIncan;

        // Pants
        public int lastedPants;
        public int pantsReinforceCount;
        public int pantsprefixIncan;
        public int pantsSuffixIncan;
    }

}