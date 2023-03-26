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
        public PlayerStatus status;

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

        #region UserInfo
        public UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.equipmentReinforcement = 0;
            userInfo.equipmentIncant = 0;
            userInfo.equipmentticket = 0;

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


        public bool GachaIncat(EquipmentItemType type, out Incant incant)
        {
            var IncantList = incantDic
                            .Where(item => item.Value.itemType == type)
                            .ToList();

            if (IncantList.Count == 0)
            {
                Debug.Log("알맞는 인챈트가 없습니다.");
                incant = null;
                return false;
            }

            int randomIndex = Random.Range(0, IncantList.Count);
            incant = IncantList[randomIndex].Value;

            if(incant.itemType != type)
            {
                Debug.LogError($"잘못된 인챈트 형식 : {incant.name}은 {type}에 인챈트할 수 없습니다!");
                incant = null;
                return false;
            }

            return true;
        }

        public bool GachaIncat(EquipmentItemType type, IncantType incantType, out Incant incant)
        {
            var IncantList = incantDic
                            .Where(item => item.Value.itemType == type && item.Value.incantType == incantType)
                            .ToList();

            if (IncantList.Count == 0)
            {
                Debug.Log("알맞는 인챈트가 없습니다.");
                incant = null;
                return false;
            }

            int randomIndex = Random.Range(0, IncantList.Count);
            incant = IncantList[randomIndex].Value;

            if (incant.itemType != type)
            {
                Debug.LogError($"잘못된 인챈트 형식 : {incant.name}은 {type}에 인챈트할 수 없습니다!");
                incant = null;
                return false;
            }

            return true;
        }
    }
}