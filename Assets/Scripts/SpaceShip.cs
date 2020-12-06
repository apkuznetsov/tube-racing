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
    
    private Vector3 ControlThrust { get; set; }
    private Vector3 ControlTorque { get; set; }

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
        ControlThrust = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            ControlThrust += Vector3.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            ControlThrust -= Vector3.forward;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            ControlThrust += Vector3.right;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            ControlThrust -= Vector3.right;
        }
        
        thisRigidbody.AddRelativeForce(Time.fixedDeltaTime * thrustForce * ControlThrust, ForceMode.Force);

        float dragCoeff = thrustForce / maxLinearVelocity;
        thisRigidbody.AddForce(-thisRigidbody.velocity * (dragCoeff * Time.fixedDeltaTime), ForceMode.Force);

        ControlTorque = Vector3.zero;

        if (Input.GetKey(KeyCode.Q))
        {
            ControlTorque += Vector3.forward;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            ControlTorque -= Vector3.forward;
        }

        ControlTorque += NormalizedMousePosition;
        
        thisRigidbody.AddRelativeTorque(Time.fixedDeltaTime * torqueForce * ControlTorque, ForceMode.Force);
        
        // angular velocity limit:
        var omega = thisRigidbody.angularVelocity;
        omega.x = Mathf.Clamp(omega.x, -maxAngularVelocity, maxAngularVelocity);
        omega.y = Mathf.Clamp(omega.y, -maxAngularVelocity, maxAngularVelocity);
        omega.z = Mathf.Clamp(omega.z, -maxAngularVelocity, maxAngularVelocity);

        thisRigidbody.angularVelocity = omega;
    }

    private Vector3 NormalizedMousePosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            
            Vector3 halfScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);

            mousePos -= halfScreen;

            mousePos.x /= halfScreen.x;
            mousePos.y /= halfScreen.y;
            
            return new Vector3(-mousePos.y, mousePos.x, 0);
        }
    }
}
