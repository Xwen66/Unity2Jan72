using UnityEngine;
using Sirenix.OdinInspector; //namespace for odin stuff
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [BoxGroup("State"), SerializeField] private float _max = 100f;
    [BoxGroup("State"), SerializeField] private float _current = 100f;

    [BoxGroup("Debug"), ShowInInspector] public float Percentage => _current / _max;
    [BoxGroup("Debug"), ShowInInspector] public bool IsAlive => _current >= 1f;


    [FoldoutGroup("Events")] public UnityEvent<DamageInfo> OnDamage;
    [FoldoutGroup("Events")] public UnityEvent<DamageInfo> OnDeath;
    public void Damage(DamageInfo damageInfo)
    {
        if (!IsAlive) return;               //stop if already dead
        if (damageInfo.Amount < 1f) return; //ignore bad amounts

        // reduce health + clamp to avoid bad values 
        _current -= damageInfo.Amount;
        _current = Mathf.Clamp(_current, 0f, _max);
         
    }

    [Button("Damage Test 10%")]
    public void DamageTest()
    {
        float amount = 0.1f * _max;
        DamageInfo damageInfo = new DamageInfo(amount, gameObject, gameObject, gameObject, DamageType.Normal);
        Damage(damageInfo);
    }
}

public class DamageInfo
{
    public DamageInfo(float amount, GameObject victim, GameObject source, GameObject instigator, DamageType damageType)
    {
        Amount = amount;
        Victim = victim;
        Source = source;
        Instigator = instigator;
        DamageType = damageType;
    }

    public float Amount { get; set; }
    public GameObject Victim { get; set; } //person exploded
    public GameObject Source { get; set; } //grenade
    public GameObject Instigator {get; set;} //person that threw the grenade
    public DamageType DamageType { get; set; }
}

public enum DamageType
{
    Normal,
    Fire,
    Ice,
    Dragon,
    Emotional
}