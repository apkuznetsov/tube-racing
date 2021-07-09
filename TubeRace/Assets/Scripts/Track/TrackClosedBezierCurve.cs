using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace TubeRace
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TrackClosedBezierCurve))]
    public class TrackClosedBezierCurveEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                ((TrackClosedBezierCurve) target).GenerateTrackData();
            }
        }
    }
#endif

    public class TrackClosedBezierCurve : Track
    {
        [SerializeField] private TrackDescription trackDescription;
        [SerializeField] private BezierTrackPoint[] trackPoints;

        [SerializeField] private int division;

        [SerializeField] private Quaternion[] trackSampledRotations;
        [SerializeField] private Vector3[] trackSampledPoints;
        [SerializeField] private float[] trackSampledSegmentLengths;
        [SerializeField] private float trackSampledLength;

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

        private static IEnumerable<Quaternion> GenerateRotations(
            Transform a,
            Transform b,
            IReadOnlyList<Vector3> points)
        {
            var rotations = new List<Quaternion>();
            float t = 0;

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 direction = (points[i + 1] - points[i]).normalized;
                Vector3 up = Vector3.Lerp(a.up, b.up, t);

                Quaternion rotation = Quaternion.LookRotation(direction, up);
                rotations.Add(rotation);

                t += 1.0f / (points.Count - 1);
            }

            rotations.Add(b.rotation);
            return rotations.ToArray();
        }

        private void Awake()
        {
            if (trackDescription != null)
                trackDescription.SetLength(trackSampledLength);
        }

#if UNITY_EDITOR
        [SerializeField] private bool debugDrawBezier;
        [SerializeField] private bool debugDrawSampledPoints;

        private static Vector3[] GenerateBezierPoints(
            BezierTrackPoint a,
            BezierTrackPoint b,
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

            if (trackDescription != null)
                trackDescription.SetLength(trackSampledLength);

            EditorUtility.SetDirty(this);
        }

        private static void DrawTrackPartGizmos(BezierTrackPoint a, BezierTrackPoint b)
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

        private void DrawSampledTrackPoints()
        {
            Handles.DrawAAPolyLine(trackSampledPoints);
        }
        
        private void OnDrawGizmos()
        {
            if (debugDrawBezier)
                DrawBezierCurveGizmos();

            if (debugDrawSampledPoints)
                DrawSampledTrackPoints();
        }
#endif
    }
}