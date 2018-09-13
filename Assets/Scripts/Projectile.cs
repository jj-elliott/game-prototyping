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
    UnityEvent OnImpact;

    Collider col;
    Rigidbody rigid;

    private void Start()
    {
        
    }

    public IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(ArmingTime);
        col.enabled = true;
    }

    public void Fire(Vector3 force)
    {
        col = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        col.enabled = false;
        StartCoroutine(EnableCollider());
        rigid.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform hitObj = collision.transform;
        UnitCombat combat = hitObj.gameObject.GetComponent<UnitCombat>();

        if(combat != null)
        {
            combat.Damage(Damage);
        }
        if(OnImpact != null)
        {
            OnImpact.Invoke();
        }
        Destroy(gameObject);
    }
}
