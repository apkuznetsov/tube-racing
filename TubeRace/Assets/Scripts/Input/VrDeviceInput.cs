using System;
using UnityEngine;
using Vr;

namespace TubeRace
{
    public class VrDeviceCustomInput : CustomInput
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