using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;

    private void Update()
    {
        transform.rotation = mainCamera.rotation;
    }
}
