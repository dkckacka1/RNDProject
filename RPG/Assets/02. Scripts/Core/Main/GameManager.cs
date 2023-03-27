using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public PlayerStatus player;

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
            this.userInfo = CreateUserInfo();
            SetStatus(userInfo);

            // TEST
            //SceneManager.LoadScene("BattleScene");
        }

        #region Method_LoadData

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
        #endregion

        public void SetStatus(UserInfo userInfo)
        {
            WeaponData w_data;
            ArmorData a_data;
            HelmetData h_data;
            PantsData p_data;
            weaponDataDic.TryGetValue(userInfo.lastedWeapon, out w_data);
            armorDataDic.TryGetValue(userInfo.lastedArmor, out a_data);
            helmetDataDic.TryGetValue(userInfo.lastedHelmet, out h_data);
            pantsDataDic.TryGetValue(userInfo.lastedPants, out p_data);

            if (w_data)
            {
                Weapon weapon = new Weapon(w_data);
                player.EquipItem(weapon);
            }
            else
                Debug.LogError("Weapon is null");

            if (a_data)
            {
                Armor armor = new Armor(a_data);
                player.EquipItem(armor);
            }
            else
                Debug.LogError("Armor is null");


            if (h_data)
            {
                Helmet helmet = new Helmet(h_data);
                player.EquipItem(helmet);
            }
            else
                Debug.LogError("Helmet is null");


            if (p_data)
            {
                Pants pants = new Pants(p_data);
                player.EquipItem(pants);
            }
            else
                Debug.LogError("Pants is null");


            player.Initialize();
        }

        #region UserInfo
        public UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.itemReinforceCount = 10;
            userInfo.itemIncantCount = 10;
            userInfo.itemGachaTicket = 10;

            userInfo.lastedWeapon = 100;
            userInfo.weaponReinforceCount = 0;
            userInfo.weaponprefixIncant = -1;
            userInfo.weaponSuffixIncant = -1;

            userInfo.lastedArmor = 200;
            userInfo.armorReinforceCount = 0;
            userInfo.armorprefixIncan = -1;
            userInfo.armorSuffixIncan = -1;

            userInfo.lastedHelmet = 300;
            userInfo.helmetReinforceCount = 0;
            userInfo.helmetprefixIncan = -1;
            userInfo.helmetSuffixIncan = -1;
            userInfo.lastedPants = 400;
            userInfo.pantsReinforceCount = 0;
            userInfo.pantsprefixIncan = -1;
            userInfo.pantsSuffixIncan = -1;

            return userInfo;
        }

        // TODO : 외부에서 Userinfo를 불러오는 함수 작성필요
        public UserInfo LoadUserInfo(string path)
        {
            return null;
        }
        #endregion
    }
}