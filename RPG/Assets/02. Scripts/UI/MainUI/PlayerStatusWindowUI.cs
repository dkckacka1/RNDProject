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
                $"{MyUtility.returnSideText("최대 층 수 : ", GameManager.Instance.UserInfo.risingTopCount.ToString() + "층")}\n" +
                $"{MyUtility.returnSideText("무기 :", weaponName)}\n" +
                $"{MyUtility.returnSideText("아머 :", armorName)}\n" +
                $"{MyUtility.returnSideText("투구 :", helmetName)}\n" +
                $"{MyUtility.returnSideText("바지 :", pantsName)}";
        }

        public void UpdateStatusText()
        {
            attackStatusText.text = $"" +
                $"{MyUtility.returnSideText("공격력 :", GameManager.Instance.Player.attackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("공격 속도 :", $"초당 {GameManager.Instance.Player.attackSpeed.ToString()}회 타격")}\n" +
                $"{MyUtility.returnSideText("공격 범위 :", GameManager.Instance.Player.attackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("적중률 :", $"{GameManager.Instance.Player.attackChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 확률 :", $"{GameManager.Instance.Player.criticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 :", $"기본 공격력의 {GameManager.Instance.Player.criticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", GameManager.Instance.Player.movementSpeed.ToString())}\n";

            defenceStatusText.text = $"" +
                $"{MyUtility.returnSideText("체력 :", GameManager.Instance.Player.maxHp.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", GameManager.Instance.Player.defencePoint.ToString())}\n" +
                $"{MyUtility.returnSideText("회피율 :", $"{GameManager.Instance.Player.evasionPoint * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 회피율 :", $"{GameManager.Instance.Player.evasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 감소 :", $"{GameManager.Instance.Player.decreseCriticalDamage * 100}% 감소")}"; ;
        }
    }
}
