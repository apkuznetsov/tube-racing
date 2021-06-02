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
        [SerializeField] private BikeParameters bikeParameters;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController visualController;

        /// <summary>
        /// Управление газом. Нормализованное. От -1 до +1
        /// </summary>
        private float forwardThrustAxis;

        /// <summary>
        /// Установка значения педали газа
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val)
        {
            forwardThrustAxis = val;
        }

        /// <summary>
        /// Управление отклонением влево и вправо. Нормализованное. От -1 до +1
        /// </summary>
        private float horizontalThrustAxis;
        
        public void SetHorizontalThrustAxis(float val)
        {
            horizontalThrustAxis = val;
        }

        private void MoveBike()
        {
            float currForwardVelocity =  forwardThrustAxis * bikeParameters.maxSpeed;
            Vector3 forwardMoveDelta = transform.forward * (currForwardVelocity * Time.deltaTime);
            
            transform.position += forwardMoveDelta;
        }
        
        private void Update()
        {
            MoveBike();
        }
    }
}