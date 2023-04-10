using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Character.Equipment;

namespace RPG.Core
{
    public static class RandomSystem
    {
        public static bool GachaRandomData<T>(Dictionary<int, EquipmentData> dic ,EquipmentItemType type, out T data, int lowerTier = 0) where T : EquipmentData
        {
            var tier = GetRandomTier(Random.Range(lowerTier, 101));

            var list = dic
                .Where(data => (data.Value.equipmentType == type && data.Value.equipmentTier == tier))
                .ToList();

            int getRandomIndex = Random.Range(0, list.Count);
            if (list.Count == 0)
            {
                Debug.Log($"{tier}����� ���� {type}�� �����ϴ�.");
                data = null;
                return false;
            }

            data = list[getRandomIndex].Value as T;
            return true;
        }

        public static bool GachaRandomData<T>(Dictionary<int, T> dic, out T data, int lowerTier = 0) where T : EquipmentData
        {
            var tier = GetRandomTier(Random.Range(lowerTier, 101));


            var list = dic
                .Where(item => item.Value.equipmentTier == tier)
                .ToList();

            int getRandomIndex = Random.Range(0, list.Count);

            if(list.Count == 0)
            {
                Debug.Log($"{tier}����� ���� �������� �����ϴ�.");
                data = null;
                return false;
            }

            Debug.Log($"����Ʈ�� ���� : {list.Count}" +
                $"\n ���� ���� �ε��� : {getRandomIndex}\n" +
                $"���� ���� Ƽ�� : {tier}");

            data = list[getRandomIndex].Value;

            return true;
        }

        public static bool GachaIncant(EquipmentItemType type, Dictionary<int, Incant> dic, out Incant incant)
        {
            var IncantList = dic
                            .Where(item => item.Value.itemType == type)
                            .ToList();

            if (IncantList.Count == 0)
            {
                Debug.Log("�˸´� ��æƮ�� �����ϴ�.");
                incant = null;
                return false;
            }

            int randomIndex = Random.Range(0, IncantList.Count);
            incant = IncantList[randomIndex].Value;

            if (incant.itemType != type)
            {
                Debug.LogError($"�߸��� ��æƮ ���� : {incant.IncantName}�� {type}�� ��æƮ�� �� �����ϴ�!");
                incant = null;
                return false;
            }

            return true;
        }

        public static bool GachaIncant(EquipmentItemType type, IncantType incantType, Dictionary<int, Incant> dic, out Incant incant)
        {
            var IncantList = dic
                            .Where(item => item.Value.itemType == type && item.Value.incantType == incantType)
                            .ToList();

            if (IncantList.Count == 0)
            {
                Debug.Log("�˸´� ��æƮ�� �����ϴ�.");
                incant = null;
                return false;
            }

            int randomIndex = Random.Range(0, IncantList.Count);
            incant = IncantList[randomIndex].Value;

            if (incant.itemType != type)
            {
                Debug.LogError($"�߸��� ��æƮ ���� : {incant.IncantName}�� {type}�� ��æƮ�� �� �����ϴ�!");
                incant = null;
                return false;
            }

            return true;
        }

        public static float ReinforceCalc(Equipment equipment)
            // ��ȭ Ȯ�� ����
            // 100f = 100%, 0 = 0%
        {
            // ���� ��ȭ ��ġ
            int currentReinforceCount = equipment.reinforceCount;
            // ��ȭ ����Ȯ��
            float reinforcementSuccessProbability = 100f - ((float)currentReinforceCount / Constant.maxReinforceCount * 100);

            return reinforcementSuccessProbability;
        }

        private static EquipmentItemTier GetRandomTier(int tex)
        {
            if (tex <= 60)
            {
                return EquipmentItemTier.Normal;
            }
            else if (tex <= 90)
            {
                return EquipmentItemTier.Rare;
            }
            else if (tex <= 98)
            {
                return EquipmentItemTier.Unique;
            }
            else if (tex <= 100)
            {
                return EquipmentItemTier.Legendary;
            }

            return EquipmentItemTier.Normal;
        }
    }

}