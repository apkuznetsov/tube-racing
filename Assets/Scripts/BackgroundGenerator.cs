using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject[] prefab;

    [SerializeField] private int numInstances = 10;
    [SerializeField] private float radius = 1;

    [SerializeField] private int scaling = 1;
    
    [SerializeField] private int seed = 42;

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        var scalingVector = new Vector3(scaling, scaling, 1);
        Random.seed = seed;
        
        for (int i = 0; i < numInstances; i++)
        {
            var randomPrefabIndex = UnityEngine.Random.Range(0, prefab.Length);
            var quad = Instantiate(prefab[randomPrefabIndex]);

            quad.transform.localScale = scalingVector;
            quad.transform.Rotate(Vector3.forward * Random.Range(-180, 180), Space.Self);
            
            quad.transform.position = container.position + Random.onUnitSphere * radius;
            quad.transform.LookAt(container);
            quad.transform.forward = -quad.transform.forward;
        }
    }
}