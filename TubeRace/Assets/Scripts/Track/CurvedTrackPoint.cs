using System;
using UnityEngine;

namespace TubeRace
{
    public class CurvedTrackPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawSphere(transform.position, 10.0f);
        }
    }
}
