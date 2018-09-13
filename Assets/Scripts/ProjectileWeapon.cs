﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : UnitWeapon
{
    [SerializeField]
    float LaunchForce;

    [SerializeField]
    float CooldownTime;

    bool onCooldown = false;

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(CooldownTime);
        onCooldown = false;
    }
    protected override void Fire(Transform target)
    {
        Projectile bullet = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        Vector3 vecToTarget = (target.position - transform.position).normalized;
        bullet.Fire(vecToTarget * LaunchForce);
        onCooldown = true;
        StartCoroutine(Reload());
    }

    protected override bool CanFire(Vector3 targetPos)
    {
        return base.CanFire(targetPos) && !onCooldown;
    }
}
