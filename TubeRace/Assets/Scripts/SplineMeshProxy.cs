using SplineMesh;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace TubeRace
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SplineMeshProxy))]
    public class SplineMeshProxyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                ((SplineMeshProxy) target).UpdatePoints();
            }
        }
    }
#endif

    [RequireComponent(typeof(Spline))]
    public class SplineMeshProxy : MonoBehaviour
    {
        [SerializeField] private TrackСlosedBezierCurve trackBezier;

        [SerializeField] private BezierTrackPoint pointA;
        [SerializeField] private BezierTrackPoint pointB;
        private Spline spline;

        private void Start()
        {
            spline = GetComponent<Spline>();
        }

        public void UpdatePoints()
        {
            SplineNode node0 = spline.nodes[0];
            Transform transformA = pointA.transform;
            Vector3 positionA = transformA.position;

            node0.Position = positionA;
            node0.Direction = positionA + transformA.forward * pointA.Length;

            SplineNode node1 = spline.nodes[1];
            Transform transformB = pointB.transform;
            Vector3 positionB = transformB.position;

            node1.Position = positionB;
            node1.Direction = positionB + transformB.forward * pointB.Length;
        }
    }
}