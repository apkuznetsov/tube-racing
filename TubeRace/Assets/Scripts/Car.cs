using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    public class Car : MonoBehaviour
    {
        [SerializeField] 
        [Range(0.0f, 100.0f)]
        private float m_Mass;

        [Multiline]
        [SerializeField] private string m_ModelName;

        [SerializeField] private string m_EnginePower;

        [Range(0,10)]
        [SerializeField] private int m_NumSteeringWheels;

        [HideInInspector]
        [SerializeField] private Color m_Color;

        [SerializeField] private Vector3 m_Pos;
        [SerializeField] private Quaternion m_Rotation;

        [SerializeField] private Transform m_Wheel;
    }
}
