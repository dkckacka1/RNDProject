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
                $"�������� ���Ӱ� �����ðڽ��ϱ�?\n" +
                $"(����� ��æƮ�� ��ȭ ��ġ�� ������ϴ�.)\n" +
                $"(�븻 : {Constant.getNormalPercent}%, ���� : {Constant.getRarelPercent}, ����ũ : {Constant.getUniquePercent}, ���� : {Constant.getLegendaryPercent})";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Gacha());
        }

        public void InitIncant()
        {
            TodoText.text = $"�����ۿ� ��æƮ�� �����Ͻðڽ��ϱ�?\n" +
                $"(���ο� ���� ��æƮ ���߿� �ϳ��� ��æƮ �Ǹ� ������ ��æƮ�� ��ü�˴ϴ�.)";

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
                $"��� ���� : {item.ToStringEquipmentType()}\n" +
                $"��� ��� : {item.ToStringTier()}\n" +
                $"���� ��æƮ : {item.ToStringIncant(IncantType.prefix)}\n" +
                $"���� ��æƮ : {item.ToStringIncant(IncantType.suffix)}";

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