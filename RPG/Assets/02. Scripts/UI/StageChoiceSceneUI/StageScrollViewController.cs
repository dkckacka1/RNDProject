using RPG.Battle.Core;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stage.UI
{

    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectTransform))]
    public class StageScrollViewController : MonoBehaviour
    {
        protected List<StageData> stageDataList = new List<StageData>(); // ����Ʈ �׸��� �����͸� ����
        [SerializeField]
        protected GameObject cellBase = null; // ���� ���� ��
        [SerializeField]
        private RectOffset padding; // ��ũ���� ������ �е�
        [SerializeField]
        private float spacingHeight = 4.0f; // �� ���� ����
        [SerializeField]
        private RectOffset visibleRectPadding = null;   //visibleRect�� �е�

        private LinkedList<StageFloorUI> cells = new LinkedList<StageFloorUI>(); // �� ���� ����Ʈ

        private Rect visibleRect; // ����Ʈ �׸��� ���� ���·� ǥ���ϴ� ������ ��Ÿ���� �簢��

        private Vector2 prevScrollPos; // �ٷ� ���� ��ũ�� ��ġ�� ����

        public RectTransform CachedRectTransform => GetComponent<RectTransform>();
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

        private int nameIndex = 0;

        protected virtual void Start()
        {
            // ���� ���� ���� ��Ȱ��ȭ �صд�.
            cellBase.SetActive(false);

            // Scroll Rect ������Ʈ�� OnvalueChanged�̺�Ʈ�� �̺�Ʈ �����ʸ� �����Ѵ�.
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);

            if (GameManager.Instance != null)
            {

            }
            else
            {
                var list = Resources.LoadAll<StageData>("Data/Stage");
                foreach (var stageData in list)
                {
                    stageDataList.Add(stageData);
                }

                CachedScrollRect.SetLayoutHorizontal();
            }
            InitializeTableView();

            CachedScrollRect.verticalNormalizedPosition = 0f;
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
                for (int i = 0; i < stageDataList.Count; i++)
                {
                    float cellHeight = GetCellHeightAtIndex(i);
                    Vector2 cellBottom = cellTop + new Vector2(0.0f, cellHeight);
                    if ((cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height) ||
                        (cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height))
                    {
                        StageFloorUI cell = CreateCellForIndex(i);
                        cell.Bottom = cellTop;
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
                LinkedListNode<StageFloorUI> node = cells.First;
                UpdateCellForIndex(node.Value, node.Value.Index);
                node = node.Next;

                while (node != null)
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                    Debug.Log(node.Value.Top);
                    Debug.Log(node.Previous.Value.Bottom);
                }

                // visibleRect�� ������ �� ���� ������ ���� �ۼ��Ѵ�.
                SetFillVisibleRectWithCells();
            }
        }

        /// <summary>
        /// ���� ���̰��� �����ϴ� �Լ�
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>The cell Height at Index</returns>
        protected virtual float GetCellHeightAtIndex(int index)
        {
            // ���� ���� ��ȯ�ϴ� ó���� ����� Ŭ�������� �����Ѵ�.
            // ������ ũ�Ⱑ �ٸ� ��� ��ӹ��� Ŭ�������� �� �����Ѵ�.
            return cellBase.GetComponent<RectTransform>().sizeDelta.y;
        }

        /// <summary>
        /// ��ũ���� ���� ��ü�� ���̸� �����ϴ� �Լ�
        /// </summary>
        protected void UpdateScrollViewSize()
        {
            float contentHeight = 0.0f;
            for (int i = 0; i < stageDataList.Count; i++)
            {
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    contentHeight += spacingHeight;
                }
            }

            // ��ũ���� ������ ���̸� �����Ѵ�.
            Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
            sizeDelta.y = padding.top + contentHeight + padding.bottom;
            CachedScrollRect.content.sizeDelta = sizeDelta;
        }

        /// <summary>
        /// ���� �����ϴ� �Լ�
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>The cell ofr index.</returns>
        private StageFloorUI CreateCellForIndex(int index)
        {
            // ���� ���� ���� �̿��� ���ο� ���� �����Ѵ�.
            GameObject obj = Instantiate(cellBase) as GameObject;
            obj.name = "StageFloor " + nameIndex++;
            obj.SetActive(false);
            StageFloorUI cell = obj.GetComponent<StageFloorUI>();

            // �θ� ��Ҹ� �ٲٸ� �������̳� ũ�⸦ �Ҿ�����Ƿ� ������ �����صд�.
            Vector3 scale = cell.transform.localScale;
            Vector2 sizeDelta = cell.CachedRectTrasnfrom.sizeDelta;
            Vector2 offsetMin = cell.CachedRectTrasnfrom.offsetMin;
            Vector2 offsetMax = cell.CachedRectTrasnfrom.offsetMax;

            cell.transform.SetParent(cellBase.transform.parent);

            // ���� �����ϰ� ũ�⸦ �����Ѵ�.
            cell.transform.localScale = scale;
            cell.CachedRectTrasnfrom.sizeDelta = sizeDelta;
            cell.CachedRectTrasnfrom.offsetMin = offsetMin;
            cell.CachedRectTrasnfrom.offsetMax = offsetMax;

            // ������ �ε����� ���� ����Ʈ �׸� �����ϴ� ���� ������ �����Ѵ�.
            UpdateCellForIndex(cell, index);

            cells.AddLast(cell);

            return cell;

        }

        /// <summary>
        /// ���� ������ �����ϴ� �Լ�
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <param name="index">Index.</param>
        private void UpdateCellForIndex(StageFloorUI cell, int index)
        {
            // ���� �����ϴ� ����Ʈ �׸��� �ε����� �����Ѵ�.
            cell.Index = index;

            if (cell.Index >= 0 && cell.Index <= stageDataList.Count - 1)
            {
                // ���� �����ϴ� ����Ʈ �׸��� �ִٸ� ���� Ȱ��ȭ�ؼ� ������ �����ϰ� ���̸� �����Ѵ�.
                cell.gameObject.SetActive(true);
                cell.UpdateContent(stageDataList[cell.Index]);
                cell.Height = GetCellHeightAtIndex(cell.Index);
            }
            else
            {
                // ���� �����ϴ� ����Ʈ �׸��� ���ٸ� ���� ��Ȱ��ȭ ���� ǥ�õ��� �ʰ� �Ѵ�.
                cell.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// VisibleRect�� �����ϱ� ���� �Լ�
        /// </summary>
        private void UpdateVisibleRect()
        {
            // visibleRect�� ��ġ�� ��ũ���� ������ �������κ��� ������� ��ġ��.
            visibleRect.x = CachedScrollRect.content.anchoredPosition.x + visibleRectPadding.left;
            visibleRect.y = CachedScrollRect.content.anchoredPosition.y + visibleRectPadding.top;

            visibleRect.width = CachedRectTransform.rect.width + visibleRectPadding.left + visibleRectPadding.right;
            visibleRect.height = CachedRectTransform.rect.height + visibleRectPadding.top + visibleRectPadding.bottom;
        }


        /// <summary>
        /// VisibleRect ������ ǥ�õ� ��ŭ�� ���� �����Ͽ� ��ġ�ϴ� �Լ�
        /// </summary>
        private void SetFillVisibleRectWithCells()
        {
            if (cells.Count < 1)
            {
                return;
            }

            // ǥ�õ� ������ ���� �����ϴ� ����Ʈ �׸��� ���� ����Ʈ �׸��� �ְ�
            // ���� �� ���� visibleRect�� ������ ���´ٸ� �����ϴ� ���� �ۼ��Ѵ�.
            StageFloorUI lastCell = cells.Last.Value;
            int nextCellDataIndex = lastCell.Index + 1;
            Vector2 nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);

            while (nextCellDataIndex < stageDataList.Count && nextCellBottom.y < visibleRect.y + visibleRect.height)
            {
                StageFloorUI cell = CreateCellForIndex(nextCellDataIndex);
                cell.Bottom = nextCellBottom;

                lastCell = cell;
                nextCellDataIndex = lastCell.Index + 1;
                nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);
            }
        }


        /// <summary>
        /// ��ũ�Ѻ䰡 ���������� ȣ��Ǵ� �Լ�
        /// </summary>
        /// <param name="scrollPos">Scroll position.</param>
        private void OnScrollPosChanged(Vector2 scrollPos)
        {
            UpdateVisibleRect();
            UpdateCells((scrollPos.y < prevScrollPos.y) ? 1 : -1);
            prevScrollPos = scrollPos;
        }

        // ���� �����Ͽ� ǥ�ø� �����ϴ� �Լ�
        private void UpdateCells(int scrollDirection)
        {
            if (cells.Count < 1)
            {
                return;
            }

            if (scrollDirection > 0)
            {
                // ���� ��ũ���ϰ� ���� ���� visibleRect�� ������ �������� ���� �ִ� ����
                // �Ʒ��� ���� ������� �̵����� ������ �����Ѵ�.
                StageFloorUI lastCell = cells.Last.Value;
                while (lastCell.Bottom.y > -visibleRect.y + visibleRect.height)
                {
                    StageFloorUI firstCell = cells.First.Value;
                    UpdateCellForIndex(lastCell, firstCell.Index - 1);
                    lastCell.Top = firstCell.Bottom + new Vector2(0.0f, -spacingHeight);

                    cells.AddFirst(lastCell);
                    cells.RemoveLast();
                    lastCell = cells.Last.Value;
                }

                // visibleRect�� ������ ���� �ȿ� �� ���� ������ ���� �ۼ��Ѵ�.
                //SetFillVisibleRectWithCells();
            }
            else if (scrollDirection < 0)
            {
                StageFloorUI firstCell = cells.First.Value;
                while (firstCell.Top.y < -visibleRect.y)
                {
                    StageFloorUI lastCell = cells.Last.Value;
                    UpdateCellForIndex(firstCell, lastCell.Index + 1);
                    firstCell.Bottom = lastCell.Top + new Vector2(0.0f, spacingHeight);

                    cells.AddLast(firstCell);
                    cells.RemoveFirst();
                    firstCell = cells.First.Value;
                }
                SetFillVisibleRectWithCells();
            }
        }
    }

}
