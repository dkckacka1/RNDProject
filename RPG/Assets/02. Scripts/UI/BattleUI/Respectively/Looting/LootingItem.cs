using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Battle.Core;
using DG.Tweening;

namespace RPG.Battle.UI
{
    public class LootingItem : MonoBehaviour
    {
        public Transform targetPos;

        [SerializeField] Image rootImage;
        [SerializeField] float rootspeed;
        [SerializeField] float minDistance;
        [SerializeField] List<LootingImage> lootings;

        bool canMove = false;

        private void OnEnable()
        {
            // TODO
        }

        private void Update()
        {
            if (canMove)
            {
                if (targetPos == null) return;

                transform.position = Vector3.Lerp(transform.position, targetPos.position, rootspeed);
                if (Vector3.Distance(transform.position, targetPos.position) < minDistance)
                {
                    canMove = false;
                    BattleManager.Instance.objectPool.ReturnLootingItem(this);
                } 
            }
        }

        public void SetUp(Transform targetPos)
        {
            this.targetPos = targetPos;
        }

        public void Init(Vector3 position ,DropItemType type)
        {
            transform.position = position;

            try
            {
                rootImage.sprite = lootings.Find(item => item.type.Equals(type)).sprite;
            }
            catch
            {
                Debug.Log($"찾는 {type}의 이미지가 없습니다.");
            }
        }
    }

}