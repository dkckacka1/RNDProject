using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class UIRecycleViewController<T> : MonoBehaviour
{
    protected List<T> tableData = new List<T>(); // 리스트 항목의 데이터를 저장
    [SerializeField]
    protected GameObject cellBase = null; // 복사 원본 셀
    [SerializeField]
    private RectOffset padding; // 스크롤할 내용의 패딩
    [SerializeField]
    private float spacingHeight = 4.0f; // 각 셀의 간격
    [SerializeField]
    private RectOffset visibleRectPadding = null;   //visibleRect의 패딩

    private LinkedList<UIRecycleViewCell<T>> cells = new LinkedList<UIRecycleViewCell<T>>(); // 셀 저장 리스트

    private Rect visibleRect; // 리스트 항목을 셀의 형태로 표시하는 범위를 나타내는 사각형

    private Vector2 prevScrollPos; // 바로 전의 스크롤 위치를 저장

    public RectTransform CachedRectTransform => GetComponent<RectTransform>();
    public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

    protected virtual void Start()
    {
        // 복사 원본 셀은 비활성화 해둔다.
        cellBase.SetActive(false);

        // Scroll Rect 컴포넌트의 OnvalueChanged이벤트의 이벤트 리스너를 설정한다.
        CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);
    }

    /// <summary>
    /// 테이블 뷰를 초기화 하는 함수
    /// </summary>
    protected void InitializeTableView()
    {
        UpdateScrollViewSize(); // 스크롤할 내용의 크기를 갱신한다.
        UpdateVisibleRect(); // visibleRect를 갱신한다.

        if (cells.Count < 1)
        {
            // 셀이 하나도 없을 때는 visibleRect의 범위에 들어가는 첫 번째 리스트 항목을 찾아서
            // 그에 대응하는 셀을 작성한다.
            Vector2 cellTop = new Vector2(0.0f, -padding.top);
            for (int i = 0; i < tableData.Count; i++)
            {
                float cellHeight = GetCellHeightAtIndex(i);
                Vector2 cellBottom = cellTop + new Vector2(0.0f, -cellHeight);
                if ((cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height)         ||
                    (cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height))
                {
                    UIRecycleViewCell<T> cell = CreateCellForIndex(i);
                    cell.Top = cellTop;
                    break;
                }

                cellTop = cellBottom + new Vector2(0.0f, spacingHeight);
            }

            // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다.
            SetFillVisibleRectWithCells();
        }
        else
        {
            // 이미 셀이 있을 때는 첫 번째 셀 부터 순서대로 대응하는 리스트 항목의
            // 인덱스를 다시 설정하고 위치와 내용을 갱신한다.
            LinkedListNode<UIRecycleViewCell<T>> node = cells.First;
        }
    }

    private void SetFillVisibleRectWithCells()
    {
        throw new NotImplementedException();
    }

    private UIRecycleViewCell<T> CreateCellForIndex(int i)
    {
        throw new NotImplementedException();
    }

    private float GetCellHeightAtIndex(int i)
    {
        throw new NotImplementedException();
    }

    private void UpdateVisibleRect()
    {
        throw new NotImplementedException();
    }

    private void UpdateScrollViewSize()
    {
        throw new NotImplementedException();
    }

    private void OnScrollPosChanged(Vector2 arg0)
    {
        throw new NotImplementedException();
    }
}
