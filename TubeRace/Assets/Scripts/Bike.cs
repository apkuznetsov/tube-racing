using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Data model
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)]
        public float mass;

        [Range(0.0f, 100.0f)]
        public float thrust;

        [Range(0.0f, 100.0f)]
        public float agility;

        public float maxSpeed;
        public bool afterburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller. Entity
    /// </summary>
    public class Bike : MonoBehaviour
    {
        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParameters m_BikeParameters;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController m_VisualController;

        private BikeParameters m_EffectiveParameters;
    }
}