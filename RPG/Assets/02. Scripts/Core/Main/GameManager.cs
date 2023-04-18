using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Character.Equipment;
using RPG.Character.Status;
using RPG.Battle.Core;
using RPG.Battle.Ability;

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
        [SerializeField] PlayerStatus player;
        public int choiceStageID;

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

        // Equipment
        public Dictionary<int, EquipmentData> equipmentDataDic = new Dictionary<int, EquipmentData>();
        public Dictionary<int, Incant> incantDic = new Dictionary<int, Incant>();

        // Skill
        public Dictionary<int, Ability> abilityPrefabDic = new Dictionary<int, Ability>();
        #endregion

        [Header("TEST")]
        [SerializeField] bool isTest;

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

            Application.targetFrameRate = 60;

            LoadEquipmentData();
            LoadEnemyData();
            LoadStageData();
            LoadSkill();

            // TEST
            this.userInfo = CreateUserInfo();
            this.player.SetPlayerStatusFromUserinfo(userInfo);

            // TEST
            //SceneManager.LoadScene("BattleScene");
        }

        #region Method_LoadData

        private void LoadEquipmentData()
        {
            ResourcesLoader.LoadEquipmentData("Data/Equipment", ref equipmentDataDic);
            ResourcesLoader.LoadIncant("Data/Incant", ref incantDic);
        }

        private void LoadEnemyData()
        {
            ResourcesLoader.LoadEnemyData("Data/Enemy", ref enemyDataDic);
        }

        private void LoadStageData()
        {
            ResourcesLoader.LoadStageData("Data/Stage", ref stageDataDic);
        }

        private void LoadSkill()
        {
            ResourcesLoader.LoadSkillPrefab("Prefab/Skill", ref abilityPrefabDic);
        }
        #endregion

        #region UserInfo
        public UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.itemReinforceTicket = 10;
            userInfo.itemIncantTicket = 10;
            userInfo.itemGachaTicket = 10;
            userInfo.risingTopCount = 1;
            userInfo.energy = 0;
            
            userInfo.lastedWeaponID = 100;
            userInfo.weaponReinforceCount = 0;
            userInfo.weaponPrefixIncantID = -1;
            userInfo.weaponSuffixIncantID = -1;

            userInfo.lastedArmorID = 200;
            userInfo.armorReinforceCount = 0;
            userInfo.armorPrefixIncantID = -1;
            userInfo.armorSuffixIncantID = -1;

            userInfo.lastedHelmetID = 300;
            userInfo.helmetReinforceCount = 0;
            userInfo.helmetPrefixIncantID = -1;
            userInfo.helmetSuffixIncantID = -1;

            userInfo.lastedPantsID = 400;
            userInfo.pantsReinforceCount = 0;
            userInfo.pantsPrefixIncantID = -1;
            userInfo.pantsSuffixIncantID = -1;

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
            if (!equipmentDataDic.TryGetValue(id, out data))
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