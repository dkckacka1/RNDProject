using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;

namespace RPG.Main.UI
{
    public class ItemPopupUI : MonoBehaviour
    {
        public Equipment choiceItem;

        [Header("itemPopupProperty")]
        [SerializeField] Button excuteBtn;
        [SerializeField] TextMeshProUGUI TodoText;

        [Header("EquipmentData")]
        [SerializeField] Image equipmentImage;
        [SerializeField] TextMeshProUGUI equipmentDescText;
        [SerializeField] TextMeshProUGUI equipmentStatusText;

        [Header("SuffixIncant")]
        [SerializeField] GameObject suffixIncantDescObject;
        [SerializeField] TextMeshProUGUI suffixIncantDescText;
        [SerializeField] GameObject suffixAbilityDescObject;
        [SerializeField] Image suffixAbilityImage;
        [SerializeField] TextMeshProUGUI suffxAbilityDescText;

        [Header("PrefixIncant")]
        [SerializeField] GameObject prefixIncantDescObject;
        [SerializeField] TextMeshProUGUI prefixIncantDescText;
        [SerializeField] GameObject prefixAbilityDescObject;
        [SerializeField] Image prefixAbilityImage;
        [SerializeField] TextMeshProUGUI prefixAbilityDescText;

        public void InitGacha()
        {
            TodoText.text = $"" +
                $"아이템을 새롭게 뽑으시겠습니까?\n" +
                $"(적용된 인챈트와 강화 수치가 사라집니다.)\n" +
                $"(노말 : {Constant.getNormalPercent}%, 레어 : {Constant.getRarelPercent}, 유니크 : {Constant.getUniquePercent}, 전설 : {Constant.getLegendaryPercent})";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Gacha());
        }

        public void InitIncant()
        {
            TodoText.text = $"아이템에 인챈트를 적용하시겠습니까?\n" +
                $"(접두와 접미 인챈트 둘중에 하나만 인챈트 되며 기존의 인챈트는 대체됩니다.)";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Incant());
        }

        public void InitReinforce()
        {
            //TodoText.text = $"아이템을 강화하시겠습니까?\n" +
            //    $"(아이템 강화확률 : {})";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Reinforce());
        }

        public void Incant()
        {

        }

        public void Gacha()
        {

        }

        public void Reinforce()
        {

        }

        public void ChoiceItem(Equipment item)
        {
            choiceItem = item;
            ShowItem(item);
        }

        private void ShowItem(Equipment item)
        {
            equipmentImage.sprite = item.data.equipmentSprite;
            equipmentDescText.text = $"" +
                $"{MyUtility.returnSideText("장비 이름 : ", item.itemName)}\n" +
                $"장비 유형 : {item.ToStringEquipmentType()}\n" +
                $"장비 등급 : {item.ToStringTier()}\n" +
                $"접두 인챈트 : {item.ToStringIncant(IncantType.prefix)}\n" +
                $"접미 인챈트 : {item.ToStringIncant(IncantType.suffix)}";

            switch (item.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    break;
                case EquipmentItemType.Armor:
                    break;
                case EquipmentItemType.Pants:
                    break;
                case EquipmentItemType.Helmet:
                    break;
            }

            ShowIncant(item.suffix);
            ShowIncant(item.prefix);
        }

        private void ShowIncant(Incant incant)
        {
        }

        private void ShowWeaponText(Weapon weapon)
        {
            
        }
    }
}