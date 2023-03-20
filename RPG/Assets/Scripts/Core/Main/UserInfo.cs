using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class UserInfo
    {
        int equipmentReinforcement = 0; // 장비 강화권
        int equipmentIncant = 0; // 장비 인챈트권
        int equipmentticket = 0; // 장비 뽑기권

        // Weapon
        public int lastedWeapon = 100;
        // Armor
        public int lastedArmor = 200;
        // Helmet
        public int lastedHelmet = 300;
        // Pants
        public int lastedPants = 400;
    }

}