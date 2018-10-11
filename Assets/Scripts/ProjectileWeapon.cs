using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : UnitWeapon
{
    [SerializeField]
    float LaunchForce;

    [SerializeField]
    float CooldownTime;

    UnitCombat combat;

    protected override void Start()
    {
        base.Start();
        combat = GetComponentInParent<UnitCombat>();
    }
    bool onCooldown = false;

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(CooldownTime);
        onCooldown = false;
    }
    protected override void Fire(Transform target)
    {
        Projectile bullet = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        //Vector3 vecToTarget = (target.position - transform.position).normalized;
        float exitVelocity = Time.deltaTime * LaunchForce/GetComponent<Rigidbody>().mass;
        float distance = Mathf.Sqrt(Mathf.Pow(target.position.x - transform.position.x, 2)+Mathf.Pow(target.position.y - transform.position.y, 2));
        float g = 9.81F;
        float angleToFire = Mathf.Atan((Mathf.Pow(exitVelocity, 2) - Mathf.Sqrt(Mathf.Pow(exitVelocity, 4) - g * (g * distance * distance + 2*(target.position.y - transform.position.y) * Mathf.Pow(exitVelocity, 2)))) / (g * distance));
        float zDistance = Mathf.Tan(angleToFire) * distance;
        Vector3 vecToTarget = new Vector3(target.position.x-transform.position.x,target.position.y-transform.position.y,zDistance);
        bullet.Fire(vecToTarget * LaunchForce, combat);

        onCooldown = true;
        StartCoroutine(Reload());
    }

    protected override bool CanFire(Vector3 targetPos)
    {
        return base.CanFire(targetPos) && !onCooldown;
    }
}

