using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Character.Status;
using RPG.Core;

namespace RPG.Main.UI
{
    public class PlayerStatusWindowUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI userText;
        [SerializeField] TextMeshProUGUI attackStatusText;
        [SerializeField] TextMeshProUGUI defenceStatusText;

        public void Init()
        {
            UpdateUserText();
            UpdateStatusText();
        }

        public void UpdateUserText()
        {
            string weaponName = $"{(GameManager.Instance.Player.currentWeapon.prefix != null ? $"{GameManager.Instance.Player.currentWeapon.prefix.name} " : "")}" +
                $"{(GameManager.Instance.Player.currentWeapon.suffix != null ? $"{GameManager.Instance.Player.currentWeapon.suffix.name} " : "")}" +
                $"{GameManager.Instance.Player.currentWeapon.itemName} " +
                $"{((GameManager.Instance.Player.currentWeapon.reinforceCount > 0) ? $"(+{GameManager.Instance.Player.currentWeapon.reinforceCount})" : "")}";

            string armorName = $"{(GameManager.Instance.Player.currentArmor.prefix != null ? $"{GameManager.Instance.Player.currentArmor.prefix.name} " : "")}" +
                $"{(GameManager.Instance.Player.currentArmor.suffix != null ? $"{GameManager.Instance.Player.currentArmor.suffix.name} " : "")}" +
                $"{GameManager.Instance.Player.currentArmor.itemName} " +
                $"{((GameManager.Instance.Player.currentArmor.reinforceCount > 0) ? $"(+{GameManager.Instance.Player.currentArmor.reinforceCount})" : "")}";

            string helmetName = $"{(GameManager.Instance.Player.currentHelmet.prefix != null ? $"{GameManager.Instance.Player.currentHelmet.prefix.name} " : "")}" +
                $"{(GameManager.Instance.Player.currentHelmet.suffix != null ? $"{GameManager.Instance.Player.currentHelmet.suffix.name} " : "")}" +
                $"{GameManager.Instance.Player.currentHelmet.itemName} " +
                $"{((GameManager.Instance.Player.currentHelmet.reinforceCount > 0) ? $"(+{GameManager.Instance.Player.currentHelmet.reinforceCount})" : "")}";

            string pantsName = $"{(GameManager.Instance.Player.currentPants.prefix != null ? $"{GameManager.Instance.Player.currentPants.prefix.name} " : "")}" +
                $"{(GameManager.Instance.Player.currentPants.suffix != null ? $"{GameManager.Instance.Player.currentPants.suffix.name} " : "")}" +
                $"{GameManager.Instance.Player.currentPants.itemName} " +
                $"{((GameManager.Instance.Player.currentPants.reinforceCount > 0) ? $"(+{GameManager.Instance.Player.currentPants.reinforceCount})" : "")}";

            userText.text = $"" +
                $"{MyUtility.returnSideText("�ִ� �� �� : ", GameManager.Instance.UserInfo.risingTopCount.ToString() + "��")}\n" +
                $"{MyUtility.returnSideText("���� :", weaponName)}\n" +
                $"{MyUtility.returnSideText("�Ƹ� :", armorName)}\n" +
                $"{MyUtility.returnSideText("���� :", helmetName)}\n" +
                $"{MyUtility.returnSideText("���� :", pantsName)}";
        }

        public void UpdateStatusText()
        {
            attackStatusText.text = $"" +
                $"{MyUtility.returnSideText("���ݷ� :", GameManager.Instance.Player.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("���� �ӵ� :", $"�ʴ� {GameManager.Instance.Player.AttackSpeed.ToString()}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("���� ���� :", GameManager.Instance.Player.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("���߷� :", $"{GameManager.Instance.Player.AttackChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� :", $"{GameManager.Instance.Player.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ :", $"�⺻ ���ݷ��� {GameManager.Instance.Player.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("�̵� �ӵ� :", GameManager.Instance.Player.MovementSpeed.ToString())}\n";

            defenceStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� :", GameManager.Instance.Player.MaxHp.ToString())}\n" +
                $"{MyUtility.returnSideText("���� :", GameManager.Instance.Player.DefencePoint.ToString())}\n" +
                $"{MyUtility.returnSideText("ȸ���� :", $"{GameManager.Instance.Player.EvasionPoint * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ȸ���� :", $"{GameManager.Instance.Player.EvasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ ���� :", $"{GameManager.Instance.Player.DecreseCriticalDamage * 100}% ����")}"; ;
        }
    }
}
