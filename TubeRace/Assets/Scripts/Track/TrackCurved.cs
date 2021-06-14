using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace TubeRace
{
    public class TrackCurved : Track
    {
        [SerializeField] private CurvedTrackPoint[] trackPoints;

        public override float Length()
        {
            return 1.0f;
        }

        public override Vector3 Position(float distance)
        {
            return Vector3.zero;
        }

        public override Vector3 Direction(float distance)
        {
            return Vector3.zero;
        }

        private void DrawTrackPartGizmos(CurvedTrackPoint a, CurvedTrackPoint b)
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
            DrawBezierCurveGizmos();
        }
    }
}