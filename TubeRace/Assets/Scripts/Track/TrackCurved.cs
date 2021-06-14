using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace TubeRace
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TrackCurved))]
    public class TrackCurvedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                ((TrackCurved) target).GenerateTrackData();
            }
        }
    }
#endif

    public class TrackCurved : Track
    {
        [SerializeField] private CurvedTrackPoint[] trackPoints;

        [SerializeField] private int division;

        [SerializeField] private Vector3[] trackSampledPoints;
        [SerializeField] private float[] trackSampledSegmentLengths;
        [SerializeField] private float trackSampledLength;

        [SerializeField] private bool DebugDrawBezier;
        [SerializeField] private bool DebugDrawSampledPoints;

        public override float Length()
        {
            return trackSampledLength;
        }

        public override Vector3 Position(float distance)
        {
            return Vector3.zero;
        }

        public override Vector3 Direction(float distance)
        {
            return Vector3.zero;
        }

        public void GenerateTrackData()
        {
            Debug.Log("Generating track data");

            var points = new List<Vector3>();

            if (trackPoints.Length < 3)
                return;

            for (int i = 0; i < trackPoints.Length - 1; i++)
            {
                var currPoints = GenerateBezierPoints(trackPoints[i], trackPoints[i + 1], division);
                points.AddRange(currPoints);
            }

            var lastCurrPoints = GenerateBezierPoints(trackPoints[trackPoints.Length - 1], trackPoints[0], division);
            points.AddRange(lastCurrPoints);
            trackSampledPoints = points.ToArray();

            trackSampledSegmentLengths = new float[trackSampledPoints.Length - 1];
            trackSampledLength = 0;

            for (int i = 0; i < trackSampledPoints.Length - 1; i++)
            {
                Vector3 a = trackSampledPoints[i];
                Vector3 b = trackSampledPoints[i + 1];

                float segmentLength = (b - a).magnitude;
                trackSampledSegmentLengths[i] = segmentLength;
                trackSampledLength += segmentLength;
            }

            EditorUtility.SetDirty(this);
        }

        private void DrawSampledTrackPoints()
        {
            Handles.DrawAAPolyLine(trackSampledPoints);
        }

        private static IEnumerable<Vector3> GenerateBezierPoints(
            CurvedTrackPoint a,
            CurvedTrackPoint b,
            int division)
        {
            Transform aTransform = a.transform;
            Transform bTransform = b.transform;

            Vector3 aPosition = aTransform.position;
            Vector3 bPosition = bTransform.position;

            float aLength = a.Length;
            float bLength = b.Length;

            return Handles.MakeBezierPoints(
                aPosition,
                bPosition,
                aPosition + aTransform.forward * aLength,
                bPosition - bTransform.forward * bLength,
                division);
        }

        private static void DrawTrackPartGizmos(CurvedTrackPoint a, CurvedTrackPoint b)
        {
            Transform aTransform = a.transform;
            Transform bTransform = b.transform;

            Vector3 aPosition = aTransform.position;
            Vector3 bPosition = bTransform.position;

            float aLength = a.Length;
            float bLength = b.Length;

            Handles.DrawBezier(
                aPosition,
                bPosition,
                aPosition + aTransform.forward * aLength,
                bPosition - bTransform.forward * bLength,
                Color.green, Texture2D.whiteTexture, 1.0f);
        }

        private void DrawBezierCurveGizmos()
        {
            if (trackPoints.Length < 3)
                return;

            for (int i = 0; i < trackPoints.Length - 1; i++)
                DrawTrackPartGizmos(trackPoints[i], trackPoints[i + 1]);

            DrawTrackPartGizmos(trackPoints[trackPoints.Length - 1], trackPoints[0]);
        }

        private void OnDrawGizmos()
        {
            if (DebugDrawBezier)
                DrawBezierCurveGizmos();

            if (DebugDrawSampledPoints)
                DrawSampledTrackPoints();
        }
    }
}