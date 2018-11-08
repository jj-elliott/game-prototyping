using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : UnitWeapon
{
    [SerializeField]
    float LaunchForce;

    [SerializeField]
    float CooldownTime;

    [SerializeField]
    float offset = 1, allowedRadiusLOSCheck = 5;
    UnitCombat combat;

    protected override void Start()
    {
        base.Start();
        combat = GetComponentInParent<UnitCombat>();
    }
    bool onCooldown = false;

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(CooldownTime / (1 + 0.2f * combat.isBuffed));
        onCooldown = false;
    }
    protected override void Fire(Transform target)
    {
        Vector3 spawn = transform.parent.position + offset * (target.position - transform.parent.position).normalized;
        Projectile bullet = Instantiate(ProjectilePrefab, spawn, Quaternion.identity).GetComponent<Projectile>();
        //float exitVelocity = Time.deltaTime * LaunchForce/bullet.GetComponent<Rigidbody>().mass;
        //float distance = Mathf.Sqrt(Mathf.Pow(target.position.x - transform.position.x, 2)+Mathf.Pow(target.position.y - transform.position.y, 2));
        //float g = 9.81F;
        //float atanInput = Mathf.Pow(exitVelocity, 2) - Mathf.Sqrt(Mathf.Pow(exitVelocity, 4) - g * (g * distance * distance + 2 * (target.position.y - transform.position.y) * Mathf.Pow(exitVelocity, 2)));
        //float angleToFire = Mathf.Atan(atanInput) / (g * distance);
        //float zDistance = Mathf.Tan(angleToFire) * distance;
        //Vector3 vecToTarget;

        //if (float.IsNaN(zDistance))
        //{
        //    vecToTarget = (target.position - transform.position).normalized;
        //}
        //else
        //{
        //    vecToTarget = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, zDistance);
        //}

        bullet.Fire(target, combat);

        onCooldown = true;
        StartCoroutine(Reload());
    }

    public override bool CanFire(Vector3 targetPos)
    {
        RaycastHit hit;
        Vector3 vecToTarget = (targetPos - transform.parent.position).normalized;

        Vector3 spawn = transform.parent.position + offset * vecToTarget;
        Physics.Raycast(spawn, vecToTarget, out hit, MaxRange, LayerMask.GetMask("Default", "Units"));

        if (!hit.transform)
        {
            return false;
        }
        return base.CanFire(targetPos) && !onCooldown && (hit.transform.position - targetPos).magnitude < allowedRadiusLOSCheck;
    }
}

