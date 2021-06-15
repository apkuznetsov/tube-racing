using UnityEngine;

namespace TubeRace
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int numObjects;
        [SerializeField] private Track track;

        private void Start()
        {
            float distance = 0;

            for (int i = 0; i < numObjects; i++)
            {
                GameObject go = Instantiate(prefab);
                go.transform.position = track.Position(distance);
                go.transform.forward = track.Direction(distance);

                distance += track.Length() / numObjects;
            }
        }
    }
}