using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;
using RPG.Core;
using UnityEngine.Events;

namespace RPG.Main.UI
{
    public class ItemPopupUI : MonoBehaviour
    {
        public Equipment choiceItem;

        [Header("itemPopupProperty")]
        [SerializeField] Button incantExcuteBtn;
        [SerializeField] Button reinforceExcuteBtn;
        [SerializeField] Button gachaExcuteBtn;
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

        private VerticalLayoutGroup[] groups;

        private void Awake()
        {
            groups = GetComponentsInChildren<VerticalLayoutGroup>();
        }

        public void InitGacha()
        {
            TodoText.fontSize = 18.5f;
            TodoText.text = $"" +
                $"�������� ���Ӱ� �����ðڽ��ϱ�?\n" +
                $"(����� ��æƮ�� ��ȭ ��ġ�� ������ϴ�.)\n" +
                $"(�븻 : {Constant.getNormalPercent}%, ���� : {Constant.getRarelPercent}%, ����ũ : {Constant.getUniquePercent}%, ���� : {Constant.getLegendaryPercent}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(false);
            gachaExcuteBtn.gameObject.SetActive(true);

            
        }

        public void InitIncant()
        {
            TodoText.fontSize = 22;
            TodoText.text = $"�����ۿ� ��æƮ�� �����Ͻðڽ��ϱ�?\n" +
                $"(���ο� ���� ��æƮ ���߿� �ϳ��� ��æƮ\n" +
                $"�Ǹ� ������ ��æƮ�� ��ü�˴ϴ�.)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(true);
            reinforceExcuteBtn.gameObject.SetActive(false);
            gachaExcuteBtn.gameObject.SetActive(false);

        }

        public void InitReinforce()
        {
            TodoText.text = $"�������� ��ȭ�Ͻðڽ��ϱ�?\n" +
                $"(������ ��ȭȮ�� : {RandomSystem.ReinforceCalc(choiceItem)}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(true);
            gachaExcuteBtn.gameObject.SetActive(false);
        }

        public void InitExcuteBtn()
        {
            if (GameManager.Instance.UserInfo.itemIncantTicket <= 0)
            {
                incantExcuteBtn.interactable = false;
            }

            if (GameManager.Instance.UserInfo.itemReinforceTicket <= 0)
            {
                reinforceExcuteBtn.interactable = false;
            }

            if (GameManager.Instance.UserInfo.itemGachaTicket <= 0)
            {
                gachaExcuteBtn.interactable = false;
            }
        }
        public void Incant()
        {
            GameManager.Instance.UserInfo.itemIncantTicket--;

            Incant incant;
            RandomSystem.GachaIncant(choiceItem.equipmentType, GameManager.Instance.incantDic, out incant);

            choiceItem.Incant(incant);

            ShowItem(choiceItem);
            InitIncant();
        }

        public void Gacha()
        {
            GameManager.Instance.UserInfo.itemGachaTicket--;

            EquipmentData data;
            RandomSystem.GachaRandomData(GameManager.Instance.equipmentDataDic, choiceItem.equipmentType, out data);
            if (data == null)
            {
                return;
            }

            switch (choiceItem.equipmentType)
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

            choiceItem.ChangeData(data);

            ShowItem(choiceItem);
            InitGacha();
        }

        public void Reinforce()
        {
            GameManager.Instance.UserInfo.itemReinforceTicket--;

            choiceItem.ReinforceItem();

            ShowItem(choiceItem);
            InitReinforce();
        }

        public void Exit(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
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
                $"{MyUtility.returnSideText("��� �̸� : ", $"{((item.reinforceCount > 0) ? $"+{item.reinforceCount} " : "")}{item.itemName}")}\n" +
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

            equipmentStatusText.gameObject.SetActive(false);
            equipmentStatusText.gameObject.SetActive(true);

            if (item.prefix != null)
            {
                prefixIncantDescObject.SetActive(true);
                ShowIncant(item.prefix);
            }
            else
            {
                prefixIncantDescObject.SetActive(false);
            }

            if (item.suffix != null)
            {
                suffixIncantDescObject.SetActive(true);
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
                            prefixAbilityDescObject.SetActive(true);
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
                            suffixAbilityDescObject.SetActive(true);
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
            equipmentStatusText.text = $"{MyUtility.returnSideText("���ݷ� : ",weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("���ݼӵ� : ", $"�ʴ� {weapon.AttackSpeed}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("���ݹ��� : ", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� : ", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ : ", $"���ݷ��� {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("���߷� : ", $"{weapon.AttackChance * 100}%")}";
        }

        private void ShowArmorText(Armor armor)
        {
            equipmentStatusText.text = $"{MyUtility.returnSideText("ü�� : ", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{armor.DefencePoint}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ȸ���� : ", $"{armor.EvasionPoint}%")}";
        }

        private void ShowHelmetText(Helmet helmet)
        {
            equipmentStatusText.text = $"{MyUtility.returnSideText("ü�� : ", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ ������ : ", $"{helmet.DecreseCriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ȸ���� : ", $"{helmet.EvasionCritical * 100}%")}";
        }

        private void ShowPantsText(Pants pants)
        {
            equipmentStatusText.text = $"{MyUtility.returnSideText("ü�� : ", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", $"{pants.MovementSpeed}")}";
        }
    }
}