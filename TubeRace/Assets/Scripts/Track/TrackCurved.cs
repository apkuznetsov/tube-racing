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

        [SerializeField] private Quaternion[] trackSampledRotations;
        [SerializeField] private Vector3[] trackSampledPoints;
        [SerializeField] private float[] trackSampledSegmentLengths;
        [SerializeField] private float trackSampledLength;

        [SerializeField] private bool debugDrawBezier;
        [SerializeField] private bool debugDrawSampledPoints;

        public override float Length()
        {
            return trackSampledLength;
        }

        public override Vector3 Position(float distance)
        {
            distance = Mathf.Repeat(distance, trackSampledLength);

            for (int i = 0; i < trackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - trackSampledSegmentLengths[i];
                if (diff < 0)
                {
                    float t = distance / trackSampledSegmentLengths[i];
                    return Vector3.Lerp(trackSampledPoints[i], trackSampledPoints[i + 1], t);
                }

                distance -= trackSampledSegmentLengths[i];
            }

            return Vector3.zero;
        }

        public override Vector3 Direction(float distance)
        {
            distance = Mathf.Repeat(distance, trackSampledLength);

            for (int i = 0; i < trackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - trackSampledSegmentLengths[i];
                if (diff < 0)
                    return (trackSampledPoints[i + 1] - trackSampledPoints[i]).normalized;

                distance -= trackSampledSegmentLengths[i];
            }

            return Vector3.forward;
        }

        public override Quaternion Rotation(float distance)
        {
            distance = Mathf.Repeat(distance, trackSampledLength);

            for (int i = 0; i < trackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - trackSampledSegmentLengths[i];
                if (diff < 0)
                {
                    float t = distance / trackSampledSegmentLengths[i];

                    return Quaternion.Slerp(
                        trackSampledRotations[i],
                        trackSampledRotations[i + 1],
                        t);
                }

                distance -= trackSampledSegmentLengths[i];
            }

            return Quaternion.identity;
        }

        public void GenerateTrackData()
        {
            Debug.Log("Generating track data");

            var points = new List<Vector3>();
            var rotations = new List<Quaternion>();

            if (trackPoints.Length < 3)
                return;

            for (int i = 0; i < trackPoints.Length - 1; i++)
            {
                var newPoints = GenerateBezierPoints(trackPoints[i], trackPoints[i + 1], division);
                var newRotations = GenerateRotations(
                    trackPoints[i].transform,
                    trackPoints[i + 1].transform,
                    newPoints);

                rotations.AddRange(newRotations);
                points.AddRange(newPoints);
            }

            var lastNewPoints = GenerateBezierPoints(
                trackPoints[trackPoints.Length - 1],
                trackPoints[0],
                division);
            var lastNewRotations = GenerateRotations(
                trackPoints[trackPoints.Length - 1].transform,
                trackPoints[0].transform,
                lastNewPoints);

            points.AddRange(lastNewPoints);
            rotations.AddRange(lastNewRotations);

            trackSampledRotations = rotations.ToArray();
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

        private Quaternion[] GenerateRotations(
            Transform a,
            Transform b,
            Vector3[] points)
        {
            List<Quaternion> rotations = new List<Quaternion>();
            float t = 0;

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 dir = (points[i + 1] - points[i]).normalized;

                Vector3 up = Vector3.Lerp(a.up, b.up, t);
                Quaternion rotation = Quaternion.LookRotation(dir, up);
                rotations.Add(rotation);

                t += 1.0f / (points.Length - 1);
            }

            rotations.Add(b.rotation);
            return rotations.ToArray();
        }

        private static Vector3[] GenerateBezierPoints(
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
            if (debugDrawBezier)
                DrawBezierCurveGizmos();

            if (debugDrawSampledPoints)
                DrawSampledTrackPoints();
        }
    }
}