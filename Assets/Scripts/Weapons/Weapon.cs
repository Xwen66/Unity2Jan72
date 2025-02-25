using UnityEngine;
using Sirenix.OdinInspector;
using FMODUnity;
using FMOD;

// abstract forces us to inherit from Weapon to use it
// Unity prevents us from adding it as a component
public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Weapon")] public float Damage { get; private set; } = 5f;
    [field: SerializeField, BoxGroup("Weapon")] public float Range { get; private set; } = 5f;              // actual max range
    [field: SerializeField, BoxGroup("Weapon")] public float EffectiveRange { get; private set; } = 4f;     // target range for AI
    [field: SerializeField, BoxGroup("Weapon")] public float AttackRate { get; private set; } = 2f;         
    [field: SerializeField, BoxGroup("Weapon")] public float Duration { get; private set; } = 1f;           // how long the attack takes
    [field: SerializeField, BoxGroup("Weapon")] public DamageType DamageType { get; private set; } = DamageType.Normal;
    [field: SerializeField, BoxGroup("Weapon")] public EventReference AttackSFX { get; private set; }
    [field: SerializeField, BoxGroup("Weapon")] public string AnimationTriggerName { get; private set; }
    [field: SerializeField, BoxGroup("Weapon")] public Animator Animator { get; private set; }

    private float _lastAttackTime = -1000000f;  // setting initial cooldown time

    private void OnValidate()
    {
        if(Animator == null) Animator = GetComponentInParent<Animator>();
    }

    // attempt an attack while adhering to cooldown
    public bool TryAttack(Vector3 aimPosition, GameObject instigator, int team)
    {
        // throttle attack using cooldown
        float nextAttackTime = _lastAttackTime + 1f / AttackRate;
        if (Time.time >  nextAttackTime)
        {
            _lastAttackTime = Time.time;
            Attack(aimPosition, instigator, team);
            return true;
        }

        return false;
    }

    // perform attack logic
    protected virtual void Attack(Vector3 aimPosition, GameObject instigator, int team)
    {
        // play FMOD audio
        if (!AttackSFX.IsNull) RuntimeManager.PlayOneShot(AttackSFX, transform.position);

        // play animation (if exists)
        if (!string.IsNullOrEmpty(AnimationTriggerName)) Animator.SetTrigger(AnimationTriggerName);
    }
}