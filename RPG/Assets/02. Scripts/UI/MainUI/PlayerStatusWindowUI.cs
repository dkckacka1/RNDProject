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
            string weaponName = (status.currentWeapon.prefix != null ? $"{status.currentWeapon.prefix.name} " : "")
                + (status.currentWeapon.suffix != null ? $"{status.currentWeapon.suffix.name} " : "")
                + status.currentWeapon.itemName;

            string armorName = (status.currentArmor.prefix != null ? $"{status.currentArmor.prefix.name} " : "")
                + (status.currentArmor.suffix != null ? $"{status.currentArmor.suffix.name} " : "")
                + status.currentArmor.itemName;

            string helmetName = (status.currentHelmet.prefix != null ? $"{status.currentHelmet.prefix.name} " : "")
                + (status.currentHelmet.suffix != null ? $"{status.currentHelmet.suffix.name} " : "")
                + status.currentHelmet.itemName;

            string pantsName = (status.currentPants.prefix != null ? $"{status.currentPants.prefix.name} " : "")
                + (status.currentPants.suffix != null ? $"{status.currentPants.suffix.name} " : "")
                + status.currentPants.itemName;

            userText.text = $"���� �� ��\t\t{userInfo.risingTopCount}\n" +
                $"����\t\t{weaponName}\n" +
                $"�Ƹ�\t\t{armorName}\n" +
                $"���\t\t{helmetName}\n" +
                $"����\t\t{pantsName}";
        }

        public void UpdateStatusText()
        {
            attackStatusText.text = $"���ݷ�\t{status.attackDamage}\n" +
                $"���� �ӵ�\t�ʴ� {status.attackSpeed}ȸ Ÿ��\n" +
                $"���� ����\t{status.attackRange}\n" +
                $"���߷�\t{status.attackChance * 100}%\n" +
                $"ġ��Ÿ Ȯ��\t{status.criticalChance * 100}%\n" +
                $"ġ��Ÿ ������\t�⺻ ������ {status.criticalDamage * 100}%\n" +
                $"�̵� �ӵ�\t{status.movementSpeed}";

            defenceStatusText.text = $"�ִ� ü��\t{status.maxHp}\n" +
                $"����\t{status.defencePoint}\n" +
                $"ȸ����\t{status.evasionPoint * 100}%\n" +
                $"ġ��Ÿ ȸ����\t{status.evasionCritical * 100}%\n" +
                $"ġ��Ÿ ������ ����\t{status.decreseCriticalDamage * 100}% ����";
        }
    }
}
