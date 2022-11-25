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
        [SerializeField] private BezierTrackPoint pointA;
        [SerializeField] private BezierTrackPoint pointB;
        private Spline spline;

        public void UpdatePoints()
        {
            spline = GetComponent<Spline>();

            SplineNode nodeA = spline.nodes[0];
            Transform transformA = pointA.transform;
            Vector3 positionA = transformA.position;

            nodeA.Position = positionA;
            nodeA.Direction = positionA + transformA.forward * pointA.Length;

            SplineNode nodeB = spline.nodes[1];
            Transform transformB = pointB.transform;
            Vector3 positionB = transformB.position;

            nodeB.Position = positionB;
            nodeB.Direction = positionB + transformB.forward * pointB.Length;
        }
    }
}