using UnityEngine;

[CreateAssetMenu]
public class SpaceShipParameters : ScriptableObject
{
    [SerializeField] public float thrustForce = 25000;
    [SerializeField] public float torqueForce = 1000;

    [SerializeField] public float maxLinearVelocity = 200;
    [SerializeField] public float maxAngularVelocity = 1;
}
