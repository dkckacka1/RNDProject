using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIRecycleViewCell<T> : MonoBehaviour
    {
        public RectTransform CachedRectTrasnfrom => GetComponent<RectTransform>();

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

        public abstract void UpdateContent(T itemData);

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
