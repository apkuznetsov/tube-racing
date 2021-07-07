using System;
using UnityEngine;

namespace TubeRace
{
    public class VrDeviceInput : NewInput
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