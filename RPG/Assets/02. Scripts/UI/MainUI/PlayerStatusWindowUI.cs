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

            userText.text = $"{MyUtility.returnSideText("최대 층 수 : ", userInfo.risingTopCount.ToString() + "층")}\n" +
                $"{MyUtility.returnSideText("무기 :", weaponName)}\n" +
                $"{MyUtility.returnSideText("아머 :", armorName)}\n" +
                $"{MyUtility.returnSideText("투구 :", helmetName)}\n" +
                $"{MyUtility.returnSideText("바지 :", pantsName)}";
        }

        public void UpdateStatusText()
        {
            attackStatusText.text = $"" +
                $"{MyUtility.returnSideText("공격력 :", status.attackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("공격 속도 :", $"초당 {status.attackSpeed.ToString()}회 타격")}\n" +
                $"{MyUtility.returnSideText("공격 범위 :", status.attackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("적중률 :", $"{status.attackChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 확률 :", $"{status.criticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 :", $"기본 공격력의 {status.criticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", status.movementSpeed.ToString())}\n";

            // TODO
            defenceStatusText.text = $"최대 체력\t{status.maxHp}\n" +
                $"방어력\t{status.defencePoint}\n" +
                $"회피율\t{status.evasionPoint * 100}%\n" +
                $"치명타 회피율\t{status.evasionCritical * 100}%\n" +
                $"치명타 데미지 감소\t{status.decreseCriticalDamage * 100}% 감소";
        }
    }
}
