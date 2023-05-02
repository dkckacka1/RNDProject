using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace RPG.Core
{
    public static class GameSLManager 
    {
        public static void SaveToPlayerPrefs(UserInfo userinfo)
        {
            PlayerPrefs.SetInt("itemReinforceCount", userinfo.itemReinforceTicket);
            PlayerPrefs.SetInt("itemIncantCount", userinfo.itemIncantTicket);
            PlayerPrefs.SetInt("itemGachaTicket", userinfo.itemGachaTicket);
            PlayerPrefs.SetInt("risingTopCount", userinfo.risingTopCount);
            PlayerPrefs.SetInt("Energy", userinfo.energy);

            PlayerPrefs.SetInt("lastedWeapon", userinfo.lastedWeaponID);
            PlayerPrefs.SetInt("weaponReinforceCount", userinfo.weaponReinforceCount);
            PlayerPrefs.SetInt("weaponPreifxIncantID", userinfo.weaponPrefixIncantID);
            PlayerPrefs.SetInt("weaponSuffixIncantID", userinfo.weaponSuffixIncantID);


            PlayerPrefs.SetInt("lastedArmor", userinfo.lastedArmorID);
            PlayerPrefs.SetInt("armorReinforceCount", userinfo.armorReinforceCount);
            PlayerPrefs.SetInt("armorPrefixIncantID", userinfo.armorPrefixIncantID);
            PlayerPrefs.SetInt("armorSuffixIncantID", userinfo.armorSuffixIncantID);

            PlayerPrefs.SetInt("lastedHelmet", userinfo.lastedHelmetID);
            PlayerPrefs.SetInt("helmetReinforceCount", userinfo.helmetReinforceCount);
            PlayerPrefs.SetInt("helmetPrefixIncantID", userinfo.helmetPrefixIncantID);
            PlayerPrefs.SetInt("helmetSuffixIncantID", userinfo.helmetSuffixIncantID);

            PlayerPrefs.SetInt("lastedPants", userinfo.lastedPantsID);
            PlayerPrefs.SetInt("pantsReinforceCount", userinfo.pantsReinforceCount);
            PlayerPrefs.SetInt("pantsPrefixIncantID", userinfo.pantsPrefixIncantID);
            PlayerPrefs.SetInt("pantsSuffixIncantID", userinfo.pantsSuffixIncantID);

            PlayerPrefs.Save();
        }

        public static UserInfo LoadToPlayerPrefs()
        {
            UserInfo userData = new UserInfo();

            userData.itemReinforceTicket = PlayerPrefs.GetInt("itemReinforceCount");
            userData.itemIncantTicket = PlayerPrefs.GetInt("itemIncantCount");
            userData.itemGachaTicket = PlayerPrefs.GetInt("itemGachaTicket");
            userData.risingTopCount = PlayerPrefs.GetInt("risingTopCount");
            userData.energy = PlayerPrefs.GetInt("Energy");

            userData.lastedWeaponID = PlayerPrefs.GetInt("lastedWeapon");
            userData.weaponReinforceCount = PlayerPrefs.GetInt("weaponReinforceCount");
            userData.weaponPrefixIncantID = PlayerPrefs.GetInt("weaponPreifxIncantID");
            userData.weaponSuffixIncantID = PlayerPrefs.GetInt("weaponSuffixIncantID");

            userData.lastedArmorID = PlayerPrefs.GetInt("lastedArmor");
            userData.armorReinforceCount = PlayerPrefs.GetInt("armorReinforceCount");
            userData.armorPrefixIncantID = PlayerPrefs.GetInt("armorPrefixIncantID");
            userData.armorSuffixIncantID = PlayerPrefs.GetInt("armorSuffixIncantID");

            userData.lastedHelmetID = PlayerPrefs.GetInt("lastedHelmet");
            userData.helmetReinforceCount = PlayerPrefs.GetInt("helmetReinforceCount");
            userData.helmetPrefixIncantID = PlayerPrefs.GetInt("helmetPrefixIncantID");
            userData.helmetSuffixIncantID = PlayerPrefs.GetInt("helmetSuffixIncantID");

            userData.lastedPantsID = PlayerPrefs.GetInt("lastedPants");
            userData.pantsReinforceCount = PlayerPrefs.GetInt("pantsReinforceCount");
            userData.pantsPrefixIncantID = PlayerPrefs.GetInt("pantsPrefixIncantID");
            userData.pantsSuffixIncantID = PlayerPrefs.GetInt("pantsSuffixIncantID");

            return userData;
        }

        public static void SaveToJSON(UserInfo userinfo, string path)
        {
            var json = JsonUtility.ToJson(userinfo, true);
            
            File.WriteAllText(path, json);
        }

        public static UserInfo LoadFromJson(string path)
        {
            string json;
            if (!File.Exists(path))
            {
                json = "";
            }
            else
            {
                json = File.ReadAllText(path);
            }
            UserInfo user = JsonUtility.FromJson<UserInfo>(json);

            return user;
        }
    }
}