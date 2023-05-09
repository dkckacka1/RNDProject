using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Inventory.Default
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Option")]
        [Range(0, 10)]
        [SerializeField] private int _horizontalSlotCount = 8;      // 슬롯 가로 개수
        [Range(0, 10)]
        [SerializeField] private int _verticalSlotCount = 8;        // 슬롯 세로 개수
        [SerializeField] private float _slotMargin = 8f;            // 한 슬롯의 상하좌우 여백
        [SerializeField] private float _contentAreaPadding = 20f;   // 인벤토리 영역의 내부 여백
        [Range(32, 64)]
        [SerializeField] private float _slotSize = 64f;             // 각 슬롯의 크기

        [Header("Connected Object")]
        [SerializeField] private RectTransform _contentAreaRT;      // 슬롯들이 위치할 영역
        [SerializeField] private GameObject _slotUiPrefab;          // 슬롯의 원본 프리팹

        /// <summary>
        /// 지정된 갯수만큼 슬롯 영역 내에 슬롯들 동적 생성
        /// </summary>
        private void InitSlots()
        {
            _slotUiPrefab.TryGetComponent(out RectTransform slotRect);
            slotRect.sizeDelta = new Vector2(_slotSize, _slotSize);
        }
            
    }

}