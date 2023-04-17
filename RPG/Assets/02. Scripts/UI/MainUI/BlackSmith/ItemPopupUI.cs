using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;
using RPG.Core;

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

        private VerticalLayoutGroup[] groups;

        private void Awake()
        {
            groups = GetComponentsInChildren<VerticalLayoutGroup>();
        }

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
            TodoText.text = $"아이템에 인챈트를 적용하시겠습니까?\n" +
                $"(접두와 접미 인챈트 둘중에 하나만 인챈트\n" +
                $"되며 기존의 인챈트는 대체됩니다.)";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Incant());
        }

        public void InitReinforce()
        {
            TodoText.text = $"아이템을 강화하시겠습니까?\n" +
                $"(아이템 강화확률 : {RandomSystem.ReinforceCalc(choiceItem)}%)";

            excuteBtn.onClick.RemoveAllListeners();
            excuteBtn.onClick.AddListener(() => Reinforce());
        }

        public void Incant()
        {
            Incant incant;
            RandomSystem.GachaIncant(choiceItem.equipmentType, GameManager.Instance.incantDic, out incant);

            choiceItem.Incant(incant);

            ShowItem(choiceItem);

            Debug.Log("버튼 누름");
        }

        public void Gacha()
        {
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
        }

        public void Exit()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public void Reinforce()
        {
            choiceItem.ReinforceItem();

            TodoText.text = $"아이템을 강화하시겠습니까?\n" +
    $"(아이템 강화확률 : {RandomSystem.ReinforceCalc(choiceItem)}%)";

            ShowItem(choiceItem);
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
                $"{MyUtility.returnSideText("장비 이름 : ", $"{((item.reinforceCount > 0) ? $"+{item.reinforceCount} " : "")}{item.itemName}")}\n" +
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
            equipmentStatusText.text = $"{MyUtility.returnSideText("공격력 : ",weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("공격속도 : ", $"초당 {weapon.AttackSpeed}회 타격")}\n" +
                $"{MyUtility.returnSideText("공격범위 : ", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("치명타 확률 : ", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 : ", $"공격력의 {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("적중률 : ", $"{weapon.AttackChance * 100}%")}";
        }

        private void ShowArmorText(Armor armor)
        {
            equipmentStatusText.text = $"{MyUtility.returnSideText("체력 : ", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{armor.DefencePoint}회 타격")}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("회피율 : ", $"{armor.EvasionPoint}%")}";
        }

        private void ShowHelmetText(Helmet helmet)
        {
            equipmentStatusText.text = $"{MyUtility.returnSideText("체력 : ", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 감소율 : ", $"{helmet.DecreseCriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 회피율 : ", $"{helmet.EvasionCritical * 100}%")}";
        }

        private void ShowPantsText(Pants pants)
        {
            equipmentStatusText.text = $"{MyUtility.returnSideText("체력 : ", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", $"{pants.MovementSpeed}")}";
        }
    }
}