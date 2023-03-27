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

            userText.text = $"오른 층 수\t\t{userInfo.risingTopCount}\n" +
                $"무기\t\t{weaponName}\n" +
                $"아머\t\t{armorName}\n" +
                $"헬멧\t\t{helmetName}\n" +
                $"바지\t\t{pantsName}";
        }

        public void UpdateStatusText()
        {
            attackStatusText.text = $"공격력\t{status.attackDamage}\n" +
                $"공격 속도\t초당 {status.attackSpeed}회 타격\n" +
                $"공격 범위\t{status.attackRange}\n" +
                $"적중률\t{status.attackChance * 100}%\n" +
                $"치명타 확률\t{status.criticalChance * 100}%\n" +
                $"치명타 데미지\t기본 공격의 {status.criticalDamage * 100}%\n" +
                $"이동 속도\t{status.movementSpeed}";

            defenceStatusText.text = $"최대 체력\t{status.maxHp}\n" +
                $"방어력\t{status.defencePoint}\n" +
                $"회피율\t{status.evasionPoint * 100}%\n" +
                $"치명타 회피율\t{status.evasionCritical * 100}%\n" +
                $"치명타 데미지 감소\t{status.decreseCriticalDamage * 100}% 감소";
        }
    }
}
