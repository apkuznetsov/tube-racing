using UnityEngine;

namespace TubeRace
{
    public class Lever : ControlDevice
    {
        [SerializeField] private Transform thisMoveDirection;
        [SerializeField] private Transform thisRotationAxis;
        [SerializeField] private float speed;

        protected override void Movement(HandController hand)
        {
            Vector3 handDeltaS = hand.DeltaS();
            float moveCoeff = Vector3.Dot(thisMoveDirection.forward, handDeltaS);

            transform.rotation *= Quaternion.AngleAxis(moveCoeff * speed * Time.deltaTime, thisRotationAxis.forward);
        }
    }
}