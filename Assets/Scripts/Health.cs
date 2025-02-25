using UnityEngine;
using Sirenix.OdinInspector;    // namespace for all Odin stuff
using UnityEngine.Events;       // namespace for Unity events

public class Health : MonoBehaviour
{
    [BoxGroup("Stats"), SerializeField] private float _current = 100f;
    [BoxGroup("Stats"), SerializeField] private float _max = 100f;
    [SerializeField] private string _corpseLayer = "Corpse";

    [BoxGroup("Debug")][ShowInInspector] public float Percentage => _current / _max;
    [BoxGroup("Debug")][ShowInInspector] public bool IsAlive => _current >= 1f;

    [FoldoutGroup("Events")] public UnityEvent<DamageInfo> OnDamage;
    [FoldoutGroup("Events")] public UnityEvent<DamageInfo> OnDeath;

    public void Damage(DamageInfo damageInfo)
    {
        if (!IsAlive) return;                   // stop if already dead
        if (damageInfo.Amount < 1f) return;     // ignore bad amounts

        // reduce health + clamp to avoid bad values
        _current -= damageInfo.Amount;
        _current = Mathf.Clamp(_current, 0f, _max);

        // damage event
        OnDamage.Invoke(damageInfo);

        if(!IsAlive)
        {
            OnDeath.Invoke(damageInfo);
            // move body to corpse layer
            gameObject.layer = LayerMask.NameToLayer(_corpseLayer);
        }
    }

    [Button("Damage Test 10%")]
    public void DamageTest()
    {
        float amount = _max * 0.1f;
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
    public GameObject Victim { get; set; }          // peron exploded
    public GameObject Source { get; set; }          // grenade
    public GameObject Instigator { get; set; }      // person that threw the grenade
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