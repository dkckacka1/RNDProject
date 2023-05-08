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
        [SerializeField] TextMeshProUGUI weaponEquipmentStatusText;
        [SerializeField] TextMeshProUGUI armorEquipmentStatusText;
        [SerializeField] TextMeshProUGUI helmetEquipmentStatusText;
        [SerializeField] TextMeshProUGUI pantsEquipmentStatusText;
        [SerializeField] VerticalLayoutGroup layout;

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

        [Header("Effecter")]
        [SerializeField] UIEffecter reinforceEffecter;

        CharacterAppearance appearance;
        Animation animation;

        private void Awake()
        {
            animation = GetComponent<Animation>();
            appearance = FindObjectOfType<CharacterAppearance>();
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
            else
            {
                incantExcuteBtn.interactable = true;
            }

            if (GameManager.Instance.UserInfo.itemReinforceTicket <= 0)
            {
                reinforceExcuteBtn.interactable = false;
            }
            else
            {
                reinforceExcuteBtn.interactable = true;
            }

            if (GameManager.Instance.UserInfo.itemGachaTicket <= 0)
            {
                gachaExcuteBtn.interactable = false;
            }
            else
            {
                gachaExcuteBtn.interactable = true;
            }
        }
        public void Incant()
        {
            GameManager.Instance.UserInfo.itemIncantTicket--;

            Incant incant;
            RandomSystem.TryGachaIncant(choiceItem.equipmentType, GameManager.Instance.incantDic, out incant);

            choiceItem.Incant(incant);

            ShowItem(choiceItem);
            GameManager.Instance.Player.SetEquipment();
            InitIncant();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
        }

        public void Gacha()
        {
            GameManager.Instance.UserInfo.itemGachaTicket--;

            EquipmentData data;
            RandomSystem.TryGachaRandomData(GameManager.Instance.equipmentDataDic, choiceItem.equipmentType, out data);
            if (data == null)
            {
                return;
            }

            if (animation.isPlaying)
            {
                ShowItem(choiceItem);
                animation.Stop();
            }

            choiceItem.ChangeData(data);
            if (choiceItem.equipmentType == EquipmentItemType.Weapon)
            {
                appearance.EquipWeapon((data as WeaponData).weaponApparenceID, (data as WeaponData).weaponHandleType);
            }

            GameManager.Instance.Player.SetEquipment();
            InitGacha();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);


            animation.Play();
        }

        public void ShowItemAnim()
        {
            ShowItem(choiceItem);
        }

        public void Reinforce()
        {
            GameManager.Instance.UserInfo.itemReinforceTicket--;

            if (MyUtility.ProbailityCalc(100 - RandomSystem.ReinforceCalc(choiceItem), 0, 100))
            {
                choiceItem.ReinforceItem();
                reinforceEffecter.Play();
            }

            GameManager.Instance.Player.SetEquipment();
            ShowItem(choiceItem);
            InitReinforce();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
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

            weaponEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            armorEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            pantsEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            helmetEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            prefixAbilityDescObject.SetActive(false);
            suffixAbilityDescObject.SetActive(false);

            switch (item.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    ShowWeaponText(weaponEquipmentStatusText, item as Weapon);
                    break;
                case EquipmentItemType.Armor:
                    ShowArmorText(armorEquipmentStatusText, item as Armor);
                    break;
                case EquipmentItemType.Pants:
                    ShowPantsText(pantsEquipmentStatusText, item as Pants);
                    break;
                case EquipmentItemType.Helmet:
                    ShowHelmetText(helmetEquipmentStatusText, item as Helmet);
                    break;
            }

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

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout.transform);
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

                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)prefixIncantDescObject.transform);
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

                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)suffixIncantDescObject.transform);
                    break;
            }
        }

        private void ShowWeaponText(TextMeshProUGUI equipmentStatusText, Weapon weapon)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("���ݷ� : ", weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("���ݼӵ� : ", $"�ʴ� {weapon.AttackSpeed}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("���ݹ��� : ", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� : ", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ : ", $"���ݷ��� {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("���߷� : ", $"{weapon.AttackChance * 100}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowArmorText(TextMeshProUGUI equipmentStatusText, Armor armor)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{armor.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ȸ���� : ", $"{armor.EvasionPoint}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowHelmetText(TextMeshProUGUI equipmentStatusText, Helmet helmet)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ ������ : ", $"{helmet.DecreseCriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ȸ���� : ", $"{helmet.EvasionCritical * 100}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowPantsText(TextMeshProUGUI equipmentStatusText, Pants pants)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", $"{pants.MovementSpeed}")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }
    }
}