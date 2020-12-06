using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Destructible")]
    [SerializeField] private float maxHitPoints;

    private float hitPoints;

    private void Start()
    {
        hitPoints = maxHitPoints;
    }

    public void ApplyDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints < 0)
            Explode();
    }

    protected virtual void Explode()
    {
        Destroy(gameObject);
    }
}
