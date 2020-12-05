using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShip : Destructible
{
    [Header("Space ship")]
    [SerializeField] private float thrustForce;
    [SerializeField] private float torqueForce;

    [SerializeField] private float maxLinearVelocity;
    [SerializeField] private float maxAngularVelocity;

    private Rigidbody thisRigidbody;

    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        UpdateRigidbody();
    }

    private void UpdateRigidbody()
    {
        throw new NotImplementedException();
    }
}
