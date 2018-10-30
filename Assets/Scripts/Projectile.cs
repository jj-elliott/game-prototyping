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
    UnityEvent OnImpact;

    Collider col;
    Rigidbody rigid;
    UnitCombat owner;

    private void Start()
    {
        
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

    public void Fire(Vector3 force, UnitCombat owner)
    {
        col = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        this.owner = owner;
        col.enabled = false;
        if (ArmingTime == 0)
        {
            col.enabled = true;
        } else
        {
            StartCoroutine(EnableCollider());
        }
        rigid.AddForce(force);
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
        }
        if(OnImpact != null)
        {
            OnImpact.Invoke();
        }
        Destroy(gameObject);
    }
}
