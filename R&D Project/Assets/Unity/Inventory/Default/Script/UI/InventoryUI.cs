using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Inventory.Default
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Option")]
        [Range(0, 10)]
        [SerializeField] private int _horizontalSlotCount = 8;      // ���� ���� ����
        [Range(0, 10)]
        [SerializeField] private int _verticalSlotCount = 8;        // ���� ���� ����
        [SerializeField] private float _slotMargin = 8f;            // �� ������ �����¿� ����
        [SerializeField] private float _contentAreaPadding = 20f;   // �κ��丮 ������ ���� ����
        [Range(32, 64)]
        [SerializeField] private float _slotSize = 64f;             // �� ������ ũ��

        [Header("Connected Object")]
        [SerializeField] private RectTransform _contentAreaRT;      // ���Ե��� ��ġ�� ����
        [SerializeField] private GameObject _slotUiPrefab;          // ������ ���� ������

        /// <summary>
        /// ������ ������ŭ ���� ���� ���� ���Ե� ���� ����
        /// </summary>
        private void InitSlots()
        {
            _slotUiPrefab.TryGetComponent(out RectTransform slotRect);
            slotRect.sizeDelta = new Vector2(_slotSize, _slotSize);
        }
            
    }

}