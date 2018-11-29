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

    [SerializeField]
    ParticleSystem buffParticles;

    public int isBuffed = 0;
    public int TeamIndex { get { return unit.TeamIndex; } }
    // Use this for initialization
    protected virtual void Start()
    {
        if (unit == null)
        {
            unit = GetComponentInParent<UnitBase>();
        }
        buffParticles = GetComponent<ParticleSystem>();
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update () {
		if(isBuffed > 0 && !buffParticles.isPlaying)
        {
            buffParticles.Play();
        } else if (isBuffed == 0)
        {
            buffParticles.Stop();
        }
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
                if (unit == null)
                    return;
                if(unit.TeamIndex == 1){
                    foreach (var factory in UnitProducer.FactoryList)
                    {
                        if ((factory.TeamIndex != unit.TeamIndex) && (!factory.convertable))
                        {
                            UnitCombat comb = factory.GetComponent<UnitCombat>();
                            if(comb == null)
                            {
                                continue;
                            }
                            comb.recoverHealth(10.0f);
                        }
                    }
                }
            }
            Destroy(unit.gameObject);
        }
    }
    //for donate order
    public void selfDamage(float ammount)
    {
        CurrentHealth -= ammount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (OnDamage != null)
        {
            OnDamage.Invoke(ammount);
        }
        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(CurrentHealth / MaxHealth);
        }
        if (CurrentHealth == 0)
        {
            if (OnDeath != null)
            {
                OnDeath.Invoke();
            }
            Destroy(unit.gameObject);
        }
    }

    public void buffUnit()
    {
        isBuffed = 1;
        print("buffed");
    }
    public void debuffUnit()
    {
        isBuffed = 0;
        print("debuffed");
    }
    public void recoverHealth(float healAmount) {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }
}
