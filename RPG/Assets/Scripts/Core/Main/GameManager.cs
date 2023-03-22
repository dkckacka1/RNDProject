using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;
using RPG.Battle.Core;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        // Singletone
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("GameManager is null");
                    return null;
                }

                return instance;
            }
        }

        // Info
        public UserInfo userInfo = new UserInfo();

        #region DIC
        //Stage
        public Dictionary<int, StageData> stageDataDic = new Dictionary<int, StageData>();

        // Enemy
        public Dictionary<int, EnemyData> enemyDataDic = new Dictionary<int, EnemyData>();

        // Equipment
        public Dictionary<int, WeaponData> weaponDataDic = new Dictionary<int, WeaponData>();
        public Dictionary<int, ArmorData> armorDataDic = new Dictionary<int, ArmorData>();
        public Dictionary<int, HelmetData> helmetDataDic = new Dictionary<int, HelmetData>();
        public Dictionary<int, PantsData> pantsDataDic = new Dictionary<int, PantsData>();
        #endregion

        // Test
        public PlayerStatus status;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            DontDestroyOnLoad(this.gameObject);

            LoadEquipmentData();
            LoadEnemyData();
            LoadStageData();

            if (status != null)
            {
                SetPlayerEquipment();
            }
        }

        private void SetPlayerEquipment()
        {
            status.EquipItem(new Weapon(weaponDataDic[100]));
            status.EquipItem(new Armor(armorDataDic[200]));
            status.EquipItem(new Helmet(helmetDataDic[300]));
            status.EquipItem(new Pants(pantsDataDic[400]));
        }

        private void LoadEquipmentData()
        {
            ResourcesLoader.LoadEquipmentData("Data/Weapon", ref weaponDataDic);
            ResourcesLoader.LoadEquipmentData("Data/Armor", ref armorDataDic);
            ResourcesLoader.LoadEquipmentData("Data/Pants", ref pantsDataDic);
            ResourcesLoader.LoadEquipmentData("Data/Helmet", ref helmetDataDic);
        }

        private void LoadEnemyData()
        {
            ResourcesLoader.LoadEnemyData("Data/Enemy", ref enemyDataDic);
        }

        private void LoadStageData()
        {
            ResourcesLoader.LoadStageData("Data/Stage", ref stageDataDic);
        }
    }

}