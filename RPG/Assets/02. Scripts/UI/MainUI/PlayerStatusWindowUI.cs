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
        UserInfo userInfo;
        PlayerStatus status;

        [SerializeField] TextMeshProUGUI userText;
        [SerializeField] TextMeshProUGUI attackStatusText;
        [SerializeField] TextMeshProUGUI defenceStatusText;

        public void Init(UserInfo userInfo, PlayerStatus status)
        {
            this.userInfo = userInfo;
            this.status = status;

            UpdateUserText();
            UpdateStatusText();
        }

        public void UpdateUserText()
        {
            string weaponName = $"{(status.currentWeapon.prefix != null ? $"{status.currentWeapon.prefix.name} " : "")}" +
                $"{(status.currentWeapon.suffix != null ? $"{status.currentWeapon.suffix.name} " : "")}" +
                $"{status.currentWeapon.itemName} " +
                $"{((status.currentWeapon.reinforceCount > 0) ? $"(+{status.currentWeapon.reinforceCount})" : "")}";

            string armorName = $"{(status.currentArmor.prefix != null ? $"{status.currentArmor.prefix.name} " : "")}" +
                $"{(status.currentArmor.suffix != null ? $"{status.currentArmor.suffix.name} " : "")}" +
                $"{status.currentArmor.itemName} " +
                $"{((status.currentArmor.reinforceCount > 0) ? $"(+{status.currentArmor.reinforceCount})" : "")}";

            string helmetName = $"{(status.currentHelmet.prefix != null ? $"{status.currentHelmet.prefix.name} " : "")}" +
                $"{(status.currentHelmet.suffix != null ? $"{status.currentHelmet.suffix.name} " : "")}" +
                $"{status.currentHelmet.itemName} " +
                $"{((status.currentHelmet.reinforceCount > 0) ? $"(+{status.currentHelmet.reinforceCount})" : "")}";

            string pantsName = $"{(status.currentPants.prefix != null ? $"{status.currentPants.prefix.name} " : "")}" +
                $"{(status.currentPants.suffix != null ? $"{status.currentPants.suffix.name} " : "")}" +
                $"{status.currentPants.itemName} " +
                $"{((status.currentPants.reinforceCount > 0) ? $"(+{status.currentPants.reinforceCount})" : "")}";

            userText.text = $"{MyUtility.returnSideText("�ִ� �� �� : ", userInfo.risingTopCount.ToString() + "��")}\n" +
                $"{MyUtility.returnSideText("���� :", weaponName)}\n" +
                $"{MyUtility.returnSideText("�Ƹ� :", armorName)}\n" +
                $"{MyUtility.returnSideText("���� :", helmetName)}\n" +
                $"{MyUtility.returnSideText("���� :", pantsName)}";
        }

        public void UpdateStatusText()
        {
            attackStatusText.text = $"" +
                $"{MyUtility.returnSideText("���ݷ� :", status.attackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("���� �ӵ� :", $"�ʴ� {status.attackSpeed.ToString()}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("���� ���� :", status.attackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("���߷� :", $"{status.attackChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� :", $"{status.criticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ :", $"�⺻ ���ݷ��� {status.criticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("�̵� �ӵ� :", status.movementSpeed.ToString())}\n";

            // TODO
            defenceStatusText.text = $"�ִ� ü��\t{status.maxHp}\n" +
                $"����\t{status.defencePoint}\n" +
                $"ȸ����\t{status.evasionPoint * 100}%\n" +
                $"ġ��Ÿ ȸ����\t{status.evasionCritical * 100}%\n" +
                $"ġ��Ÿ ������ ����\t{status.decreseCriticalDamage * 100}% ����";
        }
    }
}
