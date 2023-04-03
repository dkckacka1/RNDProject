using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Battle.Core;
using DG.Tweening;

namespace RPG.Battle.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Text;
        [SerializeField] float speed;
        [SerializeField] float deleteTiming;

        private void OnEnable()
        {
            Text.DOFade(0, deleteTiming).OnComplete(() => { ReleaseText(); });
        }

        private void Update()
        {
            transform.position += (Vector3.up * speed);
        }

        public void ReleaseText()
        {
            BattleManager.GetInstance().objectPool.ReturnText(this);
        }

        public void Init(string textStr, Vector3 position)
        {
            this.Text.alpha = 1;
            this.transform.position = Camera.main.WorldToScreenPoint(position);
            this.Text.text = textStr;
        }
    }
}