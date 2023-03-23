using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Character.Equipment;

namespace RPG.Battle.Core
{
    public class BattleFactory : MonoBehaviour
    {
        public PlayerController playerController;
        public EnemyController enemyController;

        public PlayerController CreatePlayer(UserInfo userinfo, Vector3 position, Transform parent = null)
        {
            PlayerController player = Instantiate<PlayerController>(playerController, position, Quaternion.identity, parent);
            PlayerStatus status = player.GetComponent<PlayerStatus>();
            PlayerCharacterUI ui = player.GetComponent<PlayerCharacterUI>();

            SetPlayer(userinfo, ref status);
            player.Initialize();
            SetPlayerUI(ref ui);
            ui.Initialize(status);


            return player;
        }

        public void SetPlayer(UserInfo userinfo, ref PlayerStatus status)
        {
            WeaponData w_data;
            ArmorData a_data;
            HelmetData h_data;
            PantsData p_data;
            GameManager.Instance.weaponDataDic.TryGetValue(userinfo.lastedWeapon, out w_data);
            GameManager.Instance.armorDataDic.TryGetValue(userinfo.lastedArmor, out a_data);
            GameManager.Instance.helmetDataDic.TryGetValue(userinfo.lastedHelmet, out h_data);
            GameManager.Instance.pantsDataDic.TryGetValue(userinfo.lastedPants, out p_data);



            if (w_data)
            {
                Weapon weapon = new Weapon(w_data);
                status.EquipItem(weapon);
            }
            else
                Debug.LogError("Weapon is null");

            if (a_data)
            {
                Armor armor = new Armor(a_data);
                status.EquipItem(armor);
            }
            else
                Debug.LogError("Armor is null");


            if (h_data)
            {
                Helmet helmet = new Helmet(h_data);
                status.EquipItem(helmet);
            }
            else
                Debug.LogError("Helmet is null");


            if (p_data)
            {
                Pants pants = new Pants(p_data);
                status.EquipItem(pants);
            }
            else
                Debug.LogError("Pants is null");


            status.Initialize();
        }

        public void SetPlayerUI(ref PlayerCharacterUI ui)
        {
            ui.hpBar = BattleManager.GetInstance().playerHPBar;
        }

        private static int enemyCount = 1;

        public EnemyController CreateEnemy(EnemyData data, Vector3 position, Transform parent = null)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, position, Quaternion.identity, parent);
            enemy.gameObject.name = "고블리나(" + enemyCount++ + ")";
            EnemyStatus status = enemy.GetComponent<EnemyStatus>();
            EnemyCharacterUI ui = enemy.GetComponent<EnemyCharacterUI>();

            status.SetEnemyData(data);

            GameObject looks = Instantiate(data.enemyLook, enemy.gameObject.transform);

            // Enemy Initialize()
            enemy.SetAnimator(looks.GetComponent<Animator>());
            status.Initialize();
            enemy.Initialize();
            ui.Initialize(status);

            return enemy;
        }
    }
}