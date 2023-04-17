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
        [SerializeField] TextMeshProUGUI suffixAbilityDescText;

        [Header("PrefixIncant")]
        [SerializeField] GameObject prefixIncantDescObject;
        [SerializeField] TextMeshProUGUI prefixIncantDescText;
        [SerializeField] GameObject prefixAbilityDescObject;
        [SerializeField] Image prefixAbilityImage;
        [SerializeField] TextMeshProUGUI prefixAbilityDescText;

        public void InitGacha()
        {
            TodoText.fontSize = 18.5f;
            TodoText.text = $"" +
                $"아이템을 새롭게 뽑으시겠습니까?\n" +
                $"(적용된 인챈트와 강화 수치가 사라집니다.)\n" +
                $"(노말 : {Constant.getNormalPercent}%, 레어 : {Constant.getRarelPercent}%, 유니크 : {Constant.getUniquePercent}%, 전설 : {Constant.getLegendaryPercent}%)";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Gacha());
        }

        public void InitIncant()
        {
            TodoText.fontSize = 22;
            TodoText.text = @$"아이템에 인챈트를 적용하시겠습니까?" +
                $"(접두와 접미 인챈트 둘중에 하나만 인챈트" +
                $"되며 기존의 인챈트는 대체됩니다.)";

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
                $"{MyUtility.returnSideText("장비 유형 : ", item.ToStringEquipmentType())}\n" +
                $"{MyUtility.returnSideText("장비 등급 : ", item.ToStringTier())}\n" +
                $"{MyUtility.returnSideText("접두 인챈트 : ", item.ToStringIncant(IncantType.prefix))}\n" +
                $"{MyUtility.returnSideText("접미 인챈트 : ", item.ToStringIncant(IncantType.suffix))}";

            switch (item.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    ShowWeaponText(item as Weapon);
                    break;
                case EquipmentItemType.Armor:
                    ShowArmorText(item as Armor);
                    break;
                case EquipmentItemType.Pants:
                    ShowPantsText(item as Pants);
                    break;
                case EquipmentItemType.Helmet:
                    ShowHelmetText(item as Helmet);
                    break;
            }

            if (item.prefix != null)
            {
                ShowIncant(item.prefix);
            }
            else
            {
                prefixIncantDescObject.SetActive(false);
            }

            if (item.suffix != null)
            {
                ShowIncant(item.suffix);
            }
            else
            {
                suffixIncantDescObject.SetActive(false);
            }
        }

        private void ShowIncant(Incant incant)
        {
            switch (incant.incantType)
            {
                case IncantType.prefix:
                    {
                        prefixIncantDescText.text = $"" +
                            $"{MyUtility.returnSideText("인챈트 이름 : ", incant.incantName)}";

                        string str = incant.GetAddDesc();
                        if (str != "")
                        {
                            prefixIncantDescText.text += $"\n{MyUtility.returnSideText("증가 옵션 : ", str)}";
                        }

                        str = incant.GetMinusDesc();
                        if (str != "")
                        {
                            prefixIncantDescText.text += $"\n{MyUtility.returnSideText("감소 옵션 : ", str)}";
                        }

                        if (incant.isIncantAbility)
                        {
                            prefixAbilityImage.sprite = incant.abilityIcon;
                            prefixAbilityDescText.text = $"{incant.abilityDesc}";
                        }
                        else
                        {
                            prefixAbilityDescObject.SetActive(false);
                        }
                    }
                    break;
                case IncantType.suffix:
                    {
                        suffixIncantDescText.text = $"" +
                            $"{MyUtility.returnSideText("인챈트 이름 : ", incant.incantName)}";

                        string str = incant.GetAddDesc();
                        if (str != "")
                        {
                            suffixIncantDescText.text += $"\n{MyUtility.returnSideText("증가 옵션 : ", str)}";
                        }

                        str = incant.GetMinusDesc();
                        if (str != "")
                        {
                            suffixIncantDescText.text += $"\n{MyUtility.returnSideText("감소 옵션 : ", str)}";
                        }

                        if (incant.isIncantAbility)
                        {
                            suffixAbilityImage.sprite = incant.abilityIcon;
                            suffixAbilityDescText.text = $"{incant.abilityDesc}";
                        }
                        else
                        {
                            suffixAbilityDescObject.SetActive(false);
                        }
                    }
                    break;
            }
        }

        private void ShowWeaponText(Weapon weapon)
        {
            
        }

        private void ShowArmorText(Armor armor)
        {

        }

        private void ShowHelmetText(Helmet helmet)
        {

        }

        private void ShowPantsText(Pants pants)
        {

        }
    }
}