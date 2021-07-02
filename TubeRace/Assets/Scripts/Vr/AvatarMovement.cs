using UnityEngine;

namespace TubeRace
{
    public class AvatarMovement : MonoBehaviour
    {
        [SerializeField] private NavigationPanel navigationPanel;
        [SerializeField] private float speed;

        private void Move()
        {
            Vector3 moveDirection = navigationPanel.MoveDirection();
            transform.position += moveDirection * (Time.deltaTime * speed);
        }

        private void Update()
        {
            Move();
        }
    }
}