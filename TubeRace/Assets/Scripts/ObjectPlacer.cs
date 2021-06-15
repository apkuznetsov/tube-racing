using UnityEngine;

namespace TubeRace
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int numObjects;
        [SerializeField] private Track track;

        [SerializeField] private int seed;
        [SerializeField] private bool canRadomizeRotation;
        [Range(0.0f, 1.0f)] [SerializeField] private float skipProbability;

        private void Start()
        {
            Random.InitState(seed);
            float distance = 0;

            for (int i = 0; i < numObjects; i++)
            {
                if (!(Random.Range(0.0f, 1.0f) <= skipProbability))
                {
                    GameObject go = Instantiate(prefab);
                    go.transform.position = track.Position(distance);
                    go.transform.rotation = track.Rotation(distance);

                    if (canRadomizeRotation)
                        go.transform.Rotate(Vector3.forward, Random.Range(0, 360), Space.Self);
                }

                distance += track.Length() / numObjects;
            }
        }
    }
}