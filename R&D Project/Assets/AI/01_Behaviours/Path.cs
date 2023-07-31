using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 경로의 지점지점들을 가지고 있는 클래스
namespace AI.Behaviour
{
    // 경로를 관리하는 클래스
    public class Path : MonoBehaviour
    {
        public List<GameObject> nodes;  // 경로 오브젝트
        List<PathSegment> segments;     // 지점단위

        private void Start()
        {
            // 노드로부터 경로를 만듭니다.
            segments = GetSegments();
        }

        public List<PathSegment> GetSegments()
        {
            List<PathSegment> segments = new List<PathSegment>();
            int i;
            for (i = 0; i < nodes.Count - 1; i++)
            {
                Vector3 src = nodes[i].transform.position;      // 출발 지점
                Vector3 dst = nodes[i + 1].transform.position;  // 도착 지점
                                                                // 지점들로 경로를 만듭니다.
                PathSegment segment = new PathSegment(src, dst);
                segments.Add(segment);
            }

            // 각 경로가 모여 Path가 됩니다.
            return segments;
        }

        // 현재 위치와 마지막 지점을 계산하여 현재 지점를 반환합니다.
        public float GetParam(Vector3 position, float lastParam)
        {
            float param = 0f;                   // 반환할 지점 
            PathSegment currentSegment = null;  // 현재 에이전트가 움직이는 경로
            float tempParam = 0f;
            // 현재 이동한 거리를 계산하여 에이전트가 어느 경로에 있는지 확인합니다.
            foreach (PathSegment ps in segments)
            {
                tempParam += Vector3.Distance(ps.a, ps.b);
                if (lastParam <= tempParam)
                {
                    currentSegment = ps;
                    break;
                }
            }

            // 모든 경로의 거리값을 더한값보다 이동한 거리가 높다면 첫번째 경로를 반환합니다.
            if (currentSegment == null)
                return 0;

            // 현재 시작지점에서 얼마나 이동했는지
            Vector3 currPos = position - currentSegment.a;
            // 경로의 이동방향을 확인합니다.
            Vector3 segmentDirection = currentSegment.b - currentSegment.a;
            segmentDirection.Normalize();

            // 현재 시작지점에서의 위치와 이동방향을 투영합니다.
            Vector3 pointInSegment = Vector3.Project(currPos, segmentDirection);

            // 현재 경로의 시작지점에서 얼마나 이동했는지 계산후 반환합니다.
            param = tempParam - Vector3.Distance(currentSegment.a, currentSegment.b);
            param += pointInSegment.magnitude;

            return param;
        }

        // 들어온 지점을 벡터값으로 반환합니다.
        public Vector3 GetPosition(float param)
        {
            Vector3 position = Vector3.zero;    // 반환할 벡터값
            PathSegment currentSegment = null;  // 현재 어느 경로에 위치해있는지
            float tempParam = 0f;
            // 현재 이동한 거리를 계산하여 에이전트가 어느 경로에 있는지 확인합니다.
            foreach (PathSegment ps in segments)
            {
                tempParam += Vector3.Distance(ps.a, ps.b);
                if (param <= tempParam)
                {
                    currentSegment = ps;
                    break;
                }
            }

            // 모든 경로의 거리값을 더한값보다 모든 경로를 지나쳤으므로 Vector3.zero를 반환합니다.
            if (currentSegment == null)
                return Vector3.zero;

            // 경로의 방향을 계산합니다.
            Vector3 segmentDirection = currentSegment.b - currentSegment.a;
            segmentDirection.Normalize();

            // 현재 경로에서의 지점으로 계산합니다.
            tempParam -= Vector3.Distance(currentSegment.a, currentSegment.b);
            tempParam = param - tempParam;

            // 현재 경로의 시작지점에서 경로의 방향으로 tempParam만큼 이동한 위치값을 리턴합니다.
            position = currentSegment.a + segmentDirection * tempParam;
            return position;
        }

        // 디버깅용 기즈모
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