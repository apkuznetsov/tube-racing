using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace TubeRace
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TrackCircle))]
    public class RaceTrackRoundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                ((TrackCircle) target).GenerateTrackData();
            }
        }
    }
#endif

    public class TrackCircle : Track
    {
        [SerializeField] private float circleRadius;
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
                        trackSampledRotations[i + 1], t
                    );
                }

                distance -= trackSampledSegmentLengths[i];
            }

            return Quaternion.identity;
        }

#if UNITY_EDITOR
        [SerializeField] private bool debugDrawCircle;
        [SerializeField] private bool debugDrawSampledPoints;

        private static Quaternion GenerateRotation(Vector3 a, Vector3 b, float t)
        {
            Vector3 dir = (b - a).normalized;
            Vector3 up = Vector3.Lerp(a, b, t);

            Quaternion rotation = Quaternion.LookRotation(dir, up);
            return rotation;
        }

        private static IEnumerable<Quaternion> GenerateRotations(IReadOnlyList<Vector3> points)
        {
            var rotations = new List<Quaternion>();
            float t = 0;

            for (int i = 0; i < points.Count - 1; i++)
            {
                Quaternion rotation = GenerateRotation(points[i], points[i + 1], t);
                rotations.Add(rotation);

                t += 1.0f / (points.Count - 1);
            }

            rotations.Add(GenerateRotation(points[points.Count - 1], points[0], t));
            return rotations.ToArray();
        }

        public void GenerateTrackData()
        {
            Debug.Log("Generating track data");

            var points = new List<Vector3>();
            var rotations = new List<Quaternion>();

            float divsionf = division;
            for (int i = 0; i < division; i++)
            {
                float angle = 2.0f * Mathf.PI * i / divsionf;
                Vector3 newPoints = new Vector3(
                    Mathf.Cos(angle) * circleRadius, 0, Mathf.Sin(angle) * circleRadius);

                points.Add(newPoints);
            }

            trackSampledPoints = points.ToArray();
            rotations.AddRange(GenerateRotations(trackSampledPoints));
            trackSampledRotations = rotations.ToArray();

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

        private void DrawCircleGizmos()
        {
            Handles.DrawWireDisc(Vector3.zero, Vector3.up, circleRadius);
        }

        private void OnDrawGizmos()
        {
            if (debugDrawCircle)
                DrawCircleGizmos();

            if (debugDrawSampledPoints)
                DrawSampledTrackPoints();
        }
#endif
    }
}