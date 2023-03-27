using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class UserInfo
    {
        public int itemReinforceCount; // 장비 강화권
        public int itemIncantCount; // 장비 인챈트권
        public int itemGachaTicket; // 장비 뽑기권
        public int risingTopCount;

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