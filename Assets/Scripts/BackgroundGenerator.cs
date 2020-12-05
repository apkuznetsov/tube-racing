using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject prefab;

    [SerializeField] private int numInstances;
    [SerializeField] private float radius;

    // scaling
    // seed

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < numInstances; i++)
        {
            var quad = Instantiate(prefab);

            quad.transform.position = container.position + Random.onUnitSphere * radius;
            quad.transform.LookAt(container);
            quad.transform.forward = -quad.transform.forward;

            quad.transform.Rotate(Vector3.forward * Random.Range(-180, 180), Space.Self);
        }
    }
}