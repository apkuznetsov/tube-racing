using UnityEngine;

namespace TubeRace
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int numObjects;
        [SerializeField] private Track track;

        [SerializeField] private bool canRadomizeRotation;
        
        private void Start()
        {
            float distance = 0;

            for (int i = 0; i < numObjects; i++)
            {
                GameObject go = Instantiate(prefab);
                go.transform.position = track.Position(distance);
                go.transform.rotation = track.Rotation(distance);

                if (canRadomizeRotation)
                    go.transform.Rotate(Vector3.forward, Random.Range(0, 360), Space.Self);
                
                distance += track.Length() / numObjects;
            }
        }
    }
}