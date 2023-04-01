using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Battle.Core;

namespace RPG.Battle.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Text;
        [SerializeField] float speed;
        [SerializeField] float deleteTiming;

        private float timer;

        private void OnEnable()
        {
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            transform.position += (Vector3.up * speed);
            if (timer >= deleteTiming)
            {
                BattleManager.GetInstance().objectPool.ReturnText(this);
            }
        }

        public void SetText(string textStr, Vector3 position)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(position);
            this.Text.text = textStr;
        }
    }
}