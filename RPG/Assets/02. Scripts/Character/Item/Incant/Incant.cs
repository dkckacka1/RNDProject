using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public abstract class Incant : ScriptableObject
    {
        public int incantID;

        // 어느장비에 붙을 수 있는가?
        public EquipmentItemType itemType;
        // 접두인가? 접미인가?
        public IncantType incantType;

        // 인챈트의 이름
        public string IncantName;

        // 인챈트의 설명
        public string addDesc;
        public string minusDesc;

        // 인챈트 된 스킬
        public bool isIncantSkill;
        public int skillID;


        public abstract void IncantEquipment(Equipment equipment);

        public abstract void RemoveIncant(Equipment equipment);

        public string ShowDesc()
        {
            return $"{IncantName}\t{MyUtility.returnColorText(addDesc, Color.green)} \n\t\t {MyUtility.returnColorText(minusDesc, Color.red)}";
        }
    }

}