using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Battle.Ability;
using System;

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
                if (!dic.ContainsKey(item.ID))
                {
                    dic.Add(item.ID, item);
                }
                else
                {
                    Debug.Log("이미 인덱스 번호가 있습니다.");
                }
            }
        }

        public static void LoadEquipmentData(string path, ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(path);
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
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

        public static void LoadStageData(string path, ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(path);
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        public static void LoadIncant(string path ,ref Dictionary<int, Incant> dic)
        {
            var list = Resources.LoadAll<IncantData>(path);

            foreach (var incant in list)
            {
                // 클래스이름 만들기
                string class_name = $"RPG.Character.Equipment.{incant.className}_{incant.itemType}";
                // 클래스 이름을 통한 타입 만들기
                Type incantType = Type.GetType(class_name);
                // Activator.CreateInstance를 통하여 매개변수 없는 생성자를 통한 Incant만들기
                var incantInstance = Activator.CreateInstance(incantType) as Incant;

                incantInstance.incantID = incant.ID;
                incantInstance.incantType = incant.incantType;
                incantInstance.itemType = incant.itemType;
                incantInstance.incantName = incant.incantName;
                incantInstance.addDesc = incant.addDesc;
                incantInstance.minusDesc = incant.minusDesc;
                incantInstance.isIncantAbility = incant.isIncantAbility;
                incantInstance.abilityDesc = incant.abilityDesc;
                incantInstance.abilityIcon = incant.abilityIcon;
                dic.Add(incantInstance.incantID, incantInstance);
            }
        }

        public static void LoadSkillPrefab(string path, ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(path);
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }


        #region UnUsed
        public static void LoadIncant2(ref Dictionary<int, Incant> dic)
        {
            //int id = 1;
            //dic.Add(id, new Sharpness_Weapon(id++));
            //dic.Add(id, new Fast_Weapon(id++));
            //dic.Add(id, new Heavy_Weapon(id++));
            //dic.Add(id, new Stone_Weapon(id++));
            //dic.Add(id, new Hard_Armor(id++));
            //dic.Add(id, new Smooth_Armor(id++));
            //dic.Add(id, new Balanced_Helmet(id++));
            //dic.Add(id, new Spakling_Helmet(id++));
            //dic.Add(id, new Heavy_Pants(id++));
            //dic.Add(id, new Quick_Pants(id++));
            //dic.Add(id, new Regenerative_Armor(id++));
            //dic.Add(id, new Revenge_Armor(id++));

            int id = 1;
            dic.Add(id++, new Sharpness_Weapon());
            dic.Add(id++, new Fast_Weapon());
            dic.Add(id++, new Heavy_Weapon());
            dic.Add(id++, new Stone_Weapon());
            dic.Add(id++, new Hard_Armor());
            dic.Add(id++, new Smooth_Armor());
            dic.Add(id++, new Balanced_Helmet());
            dic.Add(id++, new Spakling_Helmet());
            dic.Add(id++, new Heavy_Pants());
            dic.Add(id++, new Quick_Pants());
            dic.Add(id++, new Regenerative_Armor());
            dic.Add(id++, new Revenge_Armor());
        }
        #endregion

    }
}
