using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Класс линейного трека
    /// </summary>
    public class TrackLinear : Track
    {
        [Header("Linear track properties")]
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        
        public override float Length()
        {
            Vector3 direction = end.position - start.position;

            return direction.magnitude;
        }

        public override Vector3 Position(float distance)
        {
            distance = Mathf.Clamp(distance, 0, Length());
            Vector3 startPosition = start.position;
            
            Vector3 direction = end.position - startPosition;
            direction = direction.normalized;
            
            return startPosition + direction * distance;
        }

        public override Vector3 Direction(float distance)
        {
            distance = Mathf.Clamp(distance, 0, Length());
            
            return (end.position - start.position).normalized;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawLine(start.position, end.position);
        }

        #region Test

        [SerializeField] private float testDistance;
        [SerializeField] private Transform testObject;

        private void OnValidate()
        {
            testObject.position = Position(testDistance);
            testObject.forward = Direction(testDistance);
        }

        #endregion
    }
}

