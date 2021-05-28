using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private float m_Mass;
        [SerializeField] private string m_ModelName;
        [SerializeField] private string m_EnginePower;
        [SerializeField] private int m_NumSteeringWheels;
        [SerializeField] private Color m_Color;
    }
}
