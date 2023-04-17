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
                $"�������� ���Ӱ� �����ðڽ��ϱ�?\n" +
                $"(����� ��æƮ�� ��ȭ ��ġ�� ������ϴ�.)\n" +
                $"(�븻 : {Constant.getNormalPercent}%, ���� : {Constant.getRarelPercent}%, ����ũ : {Constant.getUniquePercent}%, ���� : {Constant.getLegendaryPercent}%)";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Gacha());
        }

        public void InitIncant()
        {
            TodoText.fontSize = 22;
            TodoText.text = @$"�����ۿ� ��æƮ�� �����Ͻðڽ��ϱ�?" +
                $"(���ο� ���� ��æƮ ���߿� �ϳ��� ��æƮ" +
                $"�Ǹ� ������ ��æƮ�� ��ü�˴ϴ�.)";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Incant());
        }

        public void InitReinforce()
        {
            //TodoText.text = $"�������� ��ȭ�Ͻðڽ��ϱ�?\n" +
            //    $"(������ ��ȭȮ�� : {})";

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
                $"{MyUtility.returnSideText("��� �̸� : ", item.itemName)}\n" +
                $"{MyUtility.returnSideText("��� ���� : ", item.ToStringEquipmentType())}\n" +
                $"{MyUtility.returnSideText("��� ��� : ", item.ToStringTier())}\n" +
                $"{MyUtility.returnSideText("���� ��æƮ : ", item.ToStringIncant(IncantType.prefix))}\n" +
                $"{MyUtility.returnSideText("���� ��æƮ : ", item.ToStringIncant(IncantType.suffix))}";

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
                            $"{MyUtility.returnSideText("��æƮ �̸� : ", incant.incantName)}";

                        string str = incant.GetAddDesc();
                        if (str != "")
                        {
                            prefixIncantDescText.text += $"\n{MyUtility.returnSideText("���� �ɼ� : ", str)}";
                        }

                        str = incant.GetMinusDesc();
                        if (str != "")
                        {
                            prefixIncantDescText.text += $"\n{MyUtility.returnSideText("���� �ɼ� : ", str)}";
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
                            $"{MyUtility.returnSideText("��æƮ �̸� : ", incant.incantName)}";

                        string str = incant.GetAddDesc();
                        if (str != "")
                        {
                            suffixIncantDescText.text += $"\n{MyUtility.returnSideText("���� �ɼ� : ", str)}";
                        }

                        str = incant.GetMinusDesc();
                        if (str != "")
                        {
                            suffixIncantDescText.text += $"\n{MyUtility.returnSideText("���� �ɼ� : ", str)}";
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