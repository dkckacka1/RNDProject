using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ������������ ������ �ִ� Ŭ����
namespace AI.Behaviour
{
    // ��θ� �����ϴ� Ŭ����
    public class Path : MonoBehaviour
    {
        public List<GameObject> nodes;  // ��� ������Ʈ
        List<PathSegment> segments;     // ��������

        private void Start()
        {
            // ���κ��� ��θ� ����ϴ�.
            segments = GetSegments();
        }

        public List<PathSegment> GetSegments()
        {
            List<PathSegment> segments = new List<PathSegment>();
            int i;
            for (i = 0; i < nodes.Count - 1; i++)
            {
                Vector3 src = nodes[i].transform.position;      // ��� ����
                Vector3 dst = nodes[i + 1].transform.position;  // ���� ����
                                                                // ������� ��θ� ����ϴ�.
                PathSegment segment = new PathSegment(src, dst);
                segments.Add(segment);
            }

            // �� ��ΰ� �� Path�� �˴ϴ�.
            return segments;
        }

        // ���� ��ġ�� ������ ������ ����Ͽ� ���� ������ ��ȯ�մϴ�.
        public float GetParam(Vector3 position, float lastParam)
        {
            float param = 0f;                   // ��ȯ�� ���� 
            PathSegment currentSegment = null;  // ���� ������Ʈ�� �����̴� ���
            float tempParam = 0f;
            // ���� �̵��� �Ÿ��� ����Ͽ� ������Ʈ�� ��� ��ο� �ִ��� Ȯ���մϴ�.
            foreach (PathSegment ps in segments)
            {
                tempParam += Vector3.Distance(ps.a, ps.b);
                if (lastParam <= tempParam)
                {
                    currentSegment = ps;
                    break;
                }
            }

            // ��� ����� �Ÿ����� ���Ѱ����� �̵��� �Ÿ��� ���ٸ� ù��° ��θ� ��ȯ�մϴ�.
            if (currentSegment == null)
                return 0;

            // ���� ������������ �󸶳� �̵��ߴ���
            Vector3 currPos = position - currentSegment.a;
            // ����� �̵������� Ȯ���մϴ�.
            Vector3 segmentDirection = currentSegment.b - currentSegment.a;
            segmentDirection.Normalize();

            // ���� �������������� ��ġ�� �̵������� �����մϴ�.
            Vector3 pointInSegment = Vector3.Project(currPos, segmentDirection);

            // ���� ����� ������������ �󸶳� �̵��ߴ��� ����� ��ȯ�մϴ�.
            param = tempParam - Vector3.Distance(currentSegment.a, currentSegment.b);
            param += pointInSegment.magnitude;

            return param;
        }

        // ���� ������ ���Ͱ����� ��ȯ�մϴ�.
        public Vector3 GetPosition(float param)
        {
            Vector3 position = Vector3.zero;    // ��ȯ�� ���Ͱ�
            PathSegment currentSegment = null;  // ���� ��� ��ο� ��ġ���ִ���
            float tempParam = 0f;
            // ���� �̵��� �Ÿ��� ����Ͽ� ������Ʈ�� ��� ��ο� �ִ��� Ȯ���մϴ�.
            foreach (PathSegment ps in segments)
            {
                tempParam += Vector3.Distance(ps.a, ps.b);
                if (param <= tempParam)
                {
                    currentSegment = ps;
                    break;
                }
            }

            // ��� ����� �Ÿ����� ���Ѱ����� ��� ��θ� ���������Ƿ� Vector3.zero�� ��ȯ�մϴ�.
            if (currentSegment == null)
                return Vector3.zero;

            // ����� ������ ����մϴ�.
            Vector3 segmentDirection = currentSegment.b - currentSegment.a;
            segmentDirection.Normalize();

            // ���� ��ο����� �������� ����մϴ�.
            tempParam -= Vector3.Distance(currentSegment.a, currentSegment.b);
            tempParam = param - tempParam;

            // ���� ����� ������������ ����� �������� tempParam��ŭ �̵��� ��ġ���� �����մϴ�.
            position = currentSegment.a + segmentDirection * tempParam;
            return position;
        }

        // ������ �����
        private void OnDrawGizmos()
        {
            Vector3 direction;
            Color tmp = Gizmos.color;
            Gizmos.color = Color.magenta;
            int i;
            for (i = 0; i < nodes.Count - 1; i++)
            {
                Vector3 src = nodes[i].transform.position;
                Vector3 dst = nodes[i + 1].transform.position;
                direction = dst - src;
                Gizmos.DrawRay(src, direction);
            }

            Gizmos.color = tmp;
        }
    }

}