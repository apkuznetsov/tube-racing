using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float lifetime;

    private float lifeTimer;
    
    private void Update()
    {
        var thisTransform = transform;
        Vector3 step = thisTransform.forward * (velocity * Time.deltaTime);

        thisTransform.position += step;

        lifeTimer += Time.deltaTime;
        
        if (lifeTimer > lifetime)
            OnLifetimeOver();
    }

    private void OnLifetimeOver()
    {
        Destroy(gameObject);
    }
}
