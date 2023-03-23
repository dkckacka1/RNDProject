using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public Dictionary<int, Incant> incantDic = new Dictionary<int, Incant>();

        // Equipment
        public Dictionary<int, WeaponData> weaponDataDic = new Dictionary<int, WeaponData>();
        public Dictionary<int, ArmorData> armorDataDic = new Dictionary<int, ArmorData>();
        public Dictionary<int, HelmetData> helmetDataDic = new Dictionary<int, HelmetData>();
        public Dictionary<int, PantsData> pantsDataDic = new Dictionary<int, PantsData>();
        #endregion


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);

            LoadEquipmentData();
            LoadEnemyData();
            LoadStageData();

            // TEST
            //SceneManager.LoadScene("BattleScene");
        }

        private void LoadEquipmentData()
        {
            ResourcesLoader.LoadEquipmentData("Data/Weapon", ref weaponDataDic);
            ResourcesLoader.LoadEquipmentData("Data/Armor", ref armorDataDic);
            ResourcesLoader.LoadEquipmentData("Data/Pants", ref pantsDataDic);
            ResourcesLoader.LoadEquipmentData("Data/Helmet", ref helmetDataDic);
            ResourcesLoader.LoadIncant(ref incantDic);
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