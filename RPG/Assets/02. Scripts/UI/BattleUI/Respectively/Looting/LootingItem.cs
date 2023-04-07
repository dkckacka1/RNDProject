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
        [SerializeField] float bouncePointX;
        [SerializeField] float bouncePointY;
        [SerializeField] float moveTime;
        [SerializeField] List<LootingImage> lootings;

        bool canMove = false;
        public float jumpPower;

        private void OnEnable()
        {
            //Debug.Log(name + " : 생성 위치 : " + transform.position +"\n" +
            //    "가야할 위치 : " + (this.transform.position.y - bouncePointY));
            //transform.DOMoveY(this.transform.position.y, moveTime).SetEase(Ease.OutBounce);
            //transform.DOMoveX(this.transform.position.x + Random.Range(-bouncePointX, bouncePointX), moveTime).OnComplete(() => { canMove = true; });

            Vector3 jumpPosition = new Vector3(transform.position.x + Random.Range(-bouncePointX, bouncePointX), transform.position.y);
            transform.DOJump(jumpPosition, jumpPower, 3, moveTime).OnComplete(() => { canMove = true; });
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
                    BattleManager.ObjectPool.ReturnLootingItem(this);
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