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

        public void UpdatePoints()
        {

        }
    }
}