using RPG.Battle.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stage.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class StageFloorUI : MonoBehaviour
    {
        public RectTransform CachedRectTrasnfrom => GetComponent<RectTransform>();

        [SerializeField] TextMeshProUGUI stageFloorText;

        // ���� �����ϴ� ����Ʈ �׸��� �ε���
        public int Index { get; set; }

        //���� ����
        public float Height
        {
            get { return CachedRectTrasnfrom.sizeDelta.y; }
            set
            {
                Vector2 sizeDelta = CachedRectTrasnfrom.sizeDelta;
                sizeDelta.y = value;
                CachedRectTrasnfrom.sizeDelta = sizeDelta;
            }
        }

        public void UpdateContent(StageData stageData)
        {
            stageFloorText.text = stageData.ID.ToString() + "��!";
        }

        // ���� ���� ���� ��ġ
        public Vector2 Top
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                return CachedRectTrasnfrom.anchoredPosition + new Vector2(0.0f, corners[1].y); // corner[1] = �»�� ���� ��ǥ
            }
            set
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                CachedRectTrasnfrom.anchoredPosition = value - new Vector2(0.0f, corners[1].y);
            }
        }

        // ���� �Ʒ��� ���� ��ġ
        public Vector2 Bottom
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                return CachedRectTrasnfrom.anchoredPosition + new Vector2(0.0f, corners[3].y); // corner[3] = ���ϴ� ���� ��ǥ
            }
            set
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                CachedRectTrasnfrom.anchoredPosition = value - new Vector2(0.0f, corners[3].y);
            }
        }
    }
}