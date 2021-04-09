using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate;

    private float refireTime;

    public void Fire()
    {
        if (refireTime > 0)
            return;

        refireTime = fireRate;
        
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        var projectile = Instantiate(projectilePrefab);
        var projectileTransform = projectile.transform;
        
        projectileTransform.position = launchPoint.position;
        projectileTransform.rotation = launchPoint.rotation;
        
        // add SFX
        // add muzzle
        // reduce ammo
    }

    private void Update()
    {
        if (refireTime > 0)
            refireTime -= Time.deltaTime;
    }
}
