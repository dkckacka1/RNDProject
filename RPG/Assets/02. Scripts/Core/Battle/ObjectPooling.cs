using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;

namespace RPG.Battle.Core
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] GameObject battleTextPrefab;

        Canvas battleCanvas;

        // Pool
        Queue<BattleText> battleTextPool = new Queue<BattleText>();

        public void Init(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        public BattleText CreateText()
        {
            GameObject obj = Instantiate(battleTextPrefab, battleCanvas.gameObject.transform);
            BattleText text = obj.GetComponent<BattleText>();
            return text;
        }

        public BattleText GetText(string textStr, Vector3 position)
        {
            if (battleTextPool.Count > 0)
            {
                // Ǯ�� �ִ� �� ���
                BattleText text = battleTextPool.Dequeue();
                text.gameObject.SetActive(true);
                text.SetText(textStr, position);
                return text;
            }
            else
            {
                // ���� ���� Ǯ�� �ֱ�
                BattleText text = CreateText();
                battleTextPool.Enqueue(text);
                text.gameObject.SetActive(true);
                text.SetText(textStr, position);
                return text;
            }
        }

        public void ReturnText(BattleText text)
        {
            text.gameObject.SetActive(false);
            battleTextPool.Enqueue(text);
        }
    }
}