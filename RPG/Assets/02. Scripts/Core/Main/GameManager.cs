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
        private UserInfo userInfo;
        [SerializeField] private PlayerStatus player;

        // Encapsule
        public UserInfo UserInfo
        {
            get
            {
                if (userInfo == null)
                {
                    Debug.LogError("Userinfo가 NULL 입니다.");
                    return null;
                }
                return userInfo;
            }
            set => userInfo = value;
        }

        public PlayerStatus Player 
        {
            get
            { 
                if(player == null)
                {
                    Debug.LogError("PlayerStatus 가 NULL입니다.");
                    return null;
                }
                return player;
            }
            set => player = value; 
        }

        #region DIC
        //Stage
        public Dictionary<int, StageData> stageDataDic = new Dictionary<int, StageData>();

        // Enemy
        public Dictionary<int, EnemyData> enemyDataDic = new Dictionary<int, EnemyData>();
        public Dictionary<int, Incant> incantDic = new Dictionary<int, Incant>();

        // Equipment
        public Dictionary<int, EquipmentData> EquipmentDataDic = new Dictionary<int, EquipmentData>();
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
            ResourcesLoader.LoadEquipmentData("Data/", ref EquipmentDataDic);
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
            GetEquipmentData(userInfo.lastedWeapon, out w_data);
            GetEquipmentData(userInfo.lastedArmor, out a_data);
            GetEquipmentData(userInfo.lastedHelmet, out h_data);
            GetEquipmentData(userInfo.lastedPants, out p_data);

            if (w_data)
            {
                Weapon weapon = new Weapon(w_data);
                Player.EquipItem(weapon);
            }
            else
                Debug.LogError("Weapon is null");

            if (a_data)
            {
                Armor armor = new Armor(a_data);
                Player.EquipItem(armor);
            }
            else
                Debug.LogError("Armor is null");


            if (h_data)
            {
                Helmet helmet = new Helmet(h_data);
                Player.EquipItem(helmet);
            }
            else
                Debug.LogError("Helmet is null");


            if (p_data)
            {
                Pants pants = new Pants(p_data);
                Player.EquipItem(pants);
            }
            else
                Debug.LogError("Pants is null");


            Player.Initialize();
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

            Debug.Log("Userinfo 생성");
            return userInfo;
        }

        public UserInfo LoadUserInfo(string path)
        {
            return null;
        }
        #endregion

        public bool GetEquipmentData<T>(int id,out T sourceData) where T : EquipmentData
        {
            EquipmentData data;
            if (!EquipmentDataDic.TryGetValue(id, out data))
            {
                Debug.LogError("찾는 데이터가 없습니다.");
                sourceData = null;
                return false;
            }

            sourceData = data as T;
            if (sourceData == null)
            {
                Debug.LogError("찾은 데이터가 잘못된 데이터입니다.");
                return false;
            }

            return true;
        }
    }
}