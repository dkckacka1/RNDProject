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
            // 플레이어 instantiate 하기
            PlayerController player = Instantiate<PlayerController>(playerController, position, Quaternion.identity, parent);
            print(player.name + "생성");

            // 플레이어에게 장비 쥐어주기
            PlayerStatus status = player.status as PlayerStatus;
            SetPlayer(userinfo, ref status);

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
            // EnemyPrefab instantiate 하기
            EnemyController enemy = Instantiate<EnemyController>(enemyController, position, Quaternion.identity, parent);
            Debug.Log("enemy생성");

            // EnemyData 넣기
            EnemyStatus status = enemy.status as EnemyStatus;
            status.SetEnemyData(data);

            // EnemyPrefab(외형) 넣기
            Instantiate(data.enemyLook, enemy.gameObject.transform);

            // EnemyPrefab에 무기 쥐어주기

            // Enemy Initialize() 하기
            status.Initialize();

            return enemy;
        }
    }
}