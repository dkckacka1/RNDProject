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

            PlayerStatus status = player.status as PlayerStatus;
            SetPlayer(userinfo, ref status);

            player.Initialize();
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

        public EnemyController CreateEnemy(EnemyData data, Vector3 position, Transform parent = null)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, position, Quaternion.identity, parent);

            EnemyStatus status = enemy.status as EnemyStatus;
            status.SetEnemyData(data);

            GameObject looks = Instantiate(data.enemyLook, enemy.gameObject.transform);

            // TODO : EnemyPrefab에 무기 쥐어주기
            //Transform right_hand = looks.transform.Find("Hand_R");
            //Instantiate(data.weapon, right_hand);

            // Enemy Initialize() �ϱ�
            enemy.SetAnimator(looks.GetComponent<Animator>());
            status.Initialize();
            enemy.Initialize();

            return enemy;
        }
    }
}