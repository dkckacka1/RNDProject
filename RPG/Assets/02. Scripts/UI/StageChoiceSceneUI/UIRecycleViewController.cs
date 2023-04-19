using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class UIRecycleViewController<T> : MonoBehaviour
{
    protected List<T> tableData = new List<T>(); // ����Ʈ �׸��� �����͸� ����
    [SerializeField]
    protected GameObject cellBase = null; // ���� ���� ��
    [SerializeField]
    private RectOffset padding; // ��ũ���� ������ �е�
    [SerializeField]
    private float spacingHeight = 4.0f; // �� ���� ����
    [SerializeField]
    private RectOffset visibleRectPadding = null;   //visibleRect�� �е�

    private LinkedList<UIRecycleViewCell<T>> cells = new LinkedList<UIRecycleViewCell<T>>(); // �� ���� ����Ʈ

    private Rect visibleRect; // ����Ʈ �׸��� ���� ���·� ǥ���ϴ� ������ ��Ÿ���� �簢��

    private Vector2 prevScrollPos; // �ٷ� ���� ��ũ�� ��ġ�� ����

    public RectTransform CachedRectTransform => GetComponent<RectTransform>();
    public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

    protected virtual void Start()
    {
        // ���� ���� ���� ��Ȱ��ȭ �صд�.
        cellBase.SetActive(false);

        // Scroll Rect ������Ʈ�� OnvalueChanged�̺�Ʈ�� �̺�Ʈ �����ʸ� �����Ѵ�.
        CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);
    }

    /// <summary>
    /// ���̺� �並 �ʱ�ȭ �ϴ� �Լ�
    /// </summary>
    protected void InitializeTableView()
    {
        UpdateScrollViewSize(); // ��ũ���� ������ ũ�⸦ �����Ѵ�.
        UpdateVisibleRect(); // visibleRect�� �����Ѵ�.

        if (cells.Count < 1)
        {
            // ���� �ϳ��� ���� ���� visibleRect�� ������ ���� ù ��° ����Ʈ �׸��� ã�Ƽ�
            // �׿� �����ϴ� ���� �ۼ��Ѵ�.
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

            // visibleRect�� ������ �� ���� ������ ���� �ۼ��Ѵ�.
            SetFillVisibleRectWithCells();
        }
        else
        {
            // �̹� ���� ���� ���� ù ��° �� ���� ������� �����ϴ� ����Ʈ �׸���
            // �ε����� �ٽ� �����ϰ� ��ġ�� ������ �����Ѵ�.
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
