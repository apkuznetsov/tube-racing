using System;
using UnityEngine;
using Vr;

namespace TubeRace
{
    public class VrDeviceInput : Input
    {
        [SerializeField] private Lever lever;

        public override Vector3 MoveDirection()
        {
            throw new NotImplementedException();
        }

        public override bool EnableAfterburner()
        {
            throw new NotImplementedException();
        }
    }
}