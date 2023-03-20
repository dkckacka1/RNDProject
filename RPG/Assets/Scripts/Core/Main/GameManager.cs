using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

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

        // Dic
        public Dictionary<int, WeaponData> weaponDataDic = new Dictionary<int, WeaponData>();
        public Dictionary<int, ArmorData> armorDataDic = new Dictionary<int, ArmorData>();
        public Dictionary<int, HelmetData> helmetDataDic = new Dictionary<int, HelmetData>();
        public Dictionary<int, PantsData> pantsDataDic = new Dictionary<int, PantsData>();

        // Test
        public PlayerStatus status;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            LoadEquipmentData();

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
    }

}