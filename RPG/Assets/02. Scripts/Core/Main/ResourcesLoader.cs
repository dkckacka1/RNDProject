using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Battle.Core;

namespace RPG.Core
{
    public static class ResourcesLoader
    {
        public static void LoadEquipmentData<T>(string path,ref Dictionary<int,T> dic) where T : EquipmentData
        {
            var items = Resources.LoadAll<T>(path);
            foreach (var item in items)
            {
                //Debug.Log(item.EquipmentName + " Loaded");
                dic.Add(item.ID, item);
            }
        }

        public static void LoadEnemyData(string path, ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(path);
            foreach(var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        public static T LoadEquipment<T>(string equpimentName, EquipmentItemType type)
            where T : EquipmentData
        {
            string path = "Data/";

            switch (type)
            {
                case EquipmentItemType.Weapon:
                    path += ("Weapon/" + equpimentName);
                    break;
                case EquipmentItemType.Armor:
                    path += ("Armor/" + equpimentName);
                    break;
                case EquipmentItemType.Pants:
                    path += ("Pants/" + equpimentName);
                    break;
                case EquipmentItemType.Helmet:
                    path += ("Helmet/" + equpimentName);
                    break;
                    //case EquipmentType.Accessory:
                    //    path += ("Weapon/" + equpimentName);
                    //    break;
            }

            T item = Resources.Load(path) as T;

            if (item == null)
            {
                Debug.LogError("찾는 아이템이 없습니다.");
            }

            return item;
        }

        public static void LoadStageData(string path, ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(path);
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        public static void LoadIncant(ref Dictionary<int, Incant> dic)
        {
            int id = 1;
            dic.Add(id++, new Sharpness_Weapon());
            dic.Add(id++, new Fast_Weapon());
            dic.Add(id++, new Hard_Armor());
            dic.Add(id++, new Smooth_Armor());
            dic.Add(id++, new Balanced_Helemt());
            dic.Add(id++, new Spakling_Helmet());
            dic.Add(id++, new Heavy_Pants());
            dic.Add(id++, new Quick_Pants());
            dic.Add(id++, new Heavy_Weapon());
        }
    } 
}
