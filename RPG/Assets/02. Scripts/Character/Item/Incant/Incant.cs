using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public abstract class Incant
    {
        public int incantID;

        // 어느장비에 붙을 수 있는가?
        public EquipmentItemType itemType;
        // 접두인가? 접미인가?
        public IncantType incantType;

        // 인챈트의 이름
        public string incantName;

        // 인챈트 된 스킬
        public bool isIncantAbility;
        public string abilityDesc;
        public Sprite abilityIcon;
        public abstract string GetAddDesc();
        public abstract string GetMinusDesc();
    }

}