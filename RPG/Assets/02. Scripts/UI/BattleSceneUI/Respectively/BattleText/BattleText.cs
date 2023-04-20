using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using RPG.Battle.Core;
using DG.Tweening;

namespace RPG.Battle.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] float speed;
        [SerializeField] float deleteTiming;
        [SerializeField] List<DamageTextMaterial> materials;



        private void OnEnable()
        {

            text.DOFade(0, deleteTiming).OnComplete(() => { ReleaseText(); });
        }

        private void Update()
        {
            transform.position += (Vector3.up * speed * Time.deltaTime);
        }

        #region Initialize
        public void Init(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
        {
            try
            {
                text.fontMaterial = materials.Find(mat => mat.type.Equals(type)).material;
            }
            catch
            {
                Debug.Log("���׸��� ���� ����");
            }


            this.text.alpha = 1;
            this.transform.position = Camera.main.WorldToScreenPoint(position);
            this.text.text = textStr;
        }

        public void ReleaseText()
        {
            BattleManager.ObjectPool.ReturnText(this);
        } 
        #endregion


    }
}