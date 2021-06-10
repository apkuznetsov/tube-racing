using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Базовый класс трубы для гонок
    /// </summary>
    public abstract class Track : MonoBehaviour
    {
        /// <summary>
        /// Радиус трубы
        /// </summary>
        [Header("Base track properties")] [SerializeField]
        private float radius;
        public float Radius => radius;

        /// <summary>
        /// Метод возвращает длину трека
        /// </summary>
        /// <returns></returns>
        public abstract float Length();

        /// <summary>
        /// Метод возвращает позицию в 3D кривой центр-линии трубы
        /// </summary>
        /// <param name="distance">расстояние от начала трубы до её GetTrackLength</param>
        /// <returns></returns>
        public abstract Vector3 Position(float distance);

        /// <summary>
        /// Метод возвращает направление в 3D кривой центр-линии трубы. 
        /// Касательная к кривой в точке
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public abstract Vector3 Direction(float distance);
    }
}