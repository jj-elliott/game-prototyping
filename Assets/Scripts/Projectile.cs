using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour {

    [SerializeField]
    float Damage;
    [SerializeField]
    float ArmingTime;

    [SerializeField]
    float Lifetime = 5f;

    [SerializeField]
    float Magnetism = 500f;

    [SerializeField]
    UnityEvent OnImpact;

    Collider col;
    Rigidbody rigid;
    UnitCombat owner;
    Transform target;
    Vector3 vecToTarget { get { return (target.position - transform.position).normalized; } }
    Vector3 startPos;
    private void Start()
    {
        
    }

    public void Update()
    {
        if (rigid)
        {
            if (target)
            {
                Vector3 deviation = (rigid.velocity - vecToTarget * Vector3.Dot(rigid.velocity, vecToTarget)).normalized;
                rigid.AddForce((vecToTarget - deviation) * Magnetism);
            }
        }
    }
    public IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(ArmingTime);
        col.enabled = true;
    }

    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }

    public void Fire(Transform target, UnitCombat owner)
    {
        col = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        var rend = GetComponent<Renderer>();
        var trail = GetComponent<TrailRenderer>();
        this.target = target;
        rend.material.color = SelectionManager.instance.teamColors[owner.TeamIndex];
        trail.startColor = trail.endColor = SelectionManager.instance.teamColors[owner.TeamIndex];
        startPos = transform.position;
        this.owner = owner;
        col.enabled = false;
        if (ArmingTime == 0)
        {
            col.enabled = true;
        } else
        {
            StartCoroutine(EnableCollider());
        }
        rigid.AddForce(vecToTarget * 10 * Magnetism);
        rigid.useGravity = false;
        StartCoroutine(SelfDestruct());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform hitObj = collision.transform;
        UnitCombat combat = hitObj.gameObject.GetComponent<UnitCombat>();

        if(combat == owner)
        {
            return;
        }

        if(combat != null)
        {
            if(owner.isBuffed == 1)
            {
                combat.Damage(Damage + 3.0f);
            }
            else
            {
                combat.Damage(Damage);
            }
        } else
        {
            var parentComb = hitObj.parent.gameObject.GetComponent<ShieldUnit>();
            if (parentComb != null && (parentComb.TeamIndex == owner.TeamIndex || (parentComb.transform.position - startPos).magnitude < parentComb.Radius))
            {
                Physics.IgnoreCollision(collision.collider, col, true);
                return;
            }
        }
        if(OnImpact != null)
        {
            OnImpact.Invoke();
        }
        Destroy(gameObject);
    }
}
