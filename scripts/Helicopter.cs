using System;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Человек
    /// </summary>
    public class Human
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public string name;

        /// <summary>
        /// Масса
        /// </summary>
        public float mass;
    }

    /// <summary>
    /// Вертолётный винт
    /// </summary>
    public class Rotor
    {
        /// <summary>
        /// Масса, кг
        /// </summary>
        public float mass;

        /// <summary>
        /// Скорость вращения, об/мин
        /// </summary>
        public float rotationSpeed;
    }

    /// <summary>
    /// Модель данных
    /// </summary>
    [Serializable]
    public class HelicopterParameters
    {
        /// <summary>
        /// Несущий винт
        /// </summary>
        public Rotor mainRotor;

        /// <summary>
        /// Рулевой винт
        /// </summary>
        public Rotor tailRotor;

        /// <summary>
        /// Высота над уровнем моря, м
        /// </summary>
        public float altitude;

        /// <summary>
        /// Масса корпуса, кг
        /// </summary>
        [Range(3000.0f, 4000.0f)] public float mass;

        /// <summary>
        /// Скорость полёта, км/ч
        /// </summary>
        [Range(40.0f, 350.0f)] public float speed;

        /// <summary>
        /// Cкорость подъёма, км/ч
        /// </summary>
        [Range(0.0f, 15.0f)] public float liftingSpeed;

        /// <summary>
        /// Запас топлива, кг
        /// </summary>
        [Range(0.0f, 500.0f)] public float fuelMass;

        /// <summary>
        /// Расход топлива, кг/мин
        /// </summary>
        [Range(0.0f, 20.0f)] public float fuelRate;

        /// <summary>
        /// Члены экипажа
        /// </summary>
        public Human[] crew;

        /// <summary>
        /// Пассажиры
        /// </summary>
        public Human[] passengers;

        /// <summary>
        /// Масса груза, кг
        /// </summary>
        public float cargoMass;

        /// <summary>
        /// ИН
        /// </summary>
        public string id;

        /// <summary>
        /// Время полёта, с
        /// </summary>
        public long flightTime;

        /// <summary>
        /// Направление
        /// </summary>
        public Vector3 direction;
    }

    /// <summary>
    /// Контролер
    /// </summary>
    public class HelicopterController : MonoBehaviour
    {
        /// <summary>
        /// Модель данных
        /// </summary>
        [SerializeField] private HelicopterParameters parameters;

        /// <summary>
        /// Пересчёт общей массы. Используется при расчёте скоростей и расхода топлива
        /// </summary>
        private void ChangeMass()
        {
            // масса груза, экипажа, пассажиров, топлива
            // общая масса используется при расчёте скоростей
        }

        /// <summary>
        /// Пересчёт расхода топлива. Зависит от общей массы и скоростей
        /// </summary>
        private void ChangeFuelRate()
        {
        }

        /// <summary>
        /// Подъём
        /// </summary>
        private void Lift()
        {
            // mass, speed, lifting speed, fuel mass, fuel rate
            // altitude
        }

        /// <summary>
        /// Приземление
        /// </summary>
        private void Land()
        {
            // mass, speed, lifting speed, fuel mass, fuel rate
            // altitude
        }

        /// <summary>
        /// Движение вперёд
        /// </summary>
        private void Move()
        {
            // mass, speed, lifting speed, fuel mass, fuel rate
        }

        /// <summary>
        /// Поворот корпуса налево
        /// </summary>
        private void RotateLeft()
        {
            // mass, speed, lifting speed, fuel mass, fuel rate
        }

        /// <summary>
        /// Поворот корпуса направо
        /// </summary>
        private void RotateRight()
        {
            // mass, speed, lifting speed, fuel mass, fuel rate
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Lift();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Land();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Move();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                RotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                RotateRight();
            }
        }
    }
}