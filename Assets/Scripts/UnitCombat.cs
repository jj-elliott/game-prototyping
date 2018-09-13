using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDamageEvent : UnityEvent<float> { };
[System.Serializable]
public class HealthChangedEvent : UnityEvent<float> { };


public class UnitCombat : MonoBehaviour {

    [SerializeField]
    float MaxHealth;
    [SerializeField]
    UnityEvent OnDeath;
    [SerializeField]
    OnDamageEvent OnDamage;
    [SerializeField]
    HealthChangedEvent OnHealthChanged;

    float CurrentHealth;

    [SerializeField]
    protected UnitBase unit;

    // Use this for initialization
    protected virtual void Start()
    {
        if (unit == null)
        {
            unit = GetComponentInParent<UnitBase>();
        }
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Damage(float ammount)
    {
        CurrentHealth -= ammount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if(OnDamage != null)
        {
            OnDamage.Invoke(ammount);
        }
        if(OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(CurrentHealth / MaxHealth);
        }
        if(CurrentHealth == 0)
        {
            if(OnDeath != null)
            {
                OnDeath.Invoke();
            }
            Destroy(unit.gameObject);
        }
    }
}
