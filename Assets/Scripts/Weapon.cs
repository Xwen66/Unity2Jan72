// using UnityEngine;
// using Sirenix.OdinInspector;
// using FMODUnity;
// using FMOD;
// public class Weapon : MonoBehaviour
// {
//     [field: SerializeField, BoxGroup("Weapon")] public float Damage { get; private set; } = 5f;
//     [field: SerializeField, BoxGroup("Weapon")] public float Range { get; private set; } = 5f;
//     [field: SerializeField, BoxGroup("Weapon")] public float EffectiveRange { get; private set; } = 4f; // target range for AI
//     [field: SerializeField, BoxGroup("Weapon")] public float AttackRate { get; private set; } = 2f;
//     [field: SerializeField, BoxGroup("Weapon")] public float Duration { get; private set; } = 1f;       //how long the attack takes
//     [field: SerializeField, BoxGroup("Weapon")] public DamageType DamageType { get; private set; } = DamageType.Normal;
//     [field: SerializeField, BoxGroup("Weapon")] public EventReference AttackSFX { get; private set; }
//     //TODO: add feedback: SFX, Animaion
//     private float _lastAttackTime = -1000000f; //setting initial cooldown time
//     public bool TryAttack(Vector3 aimPostion, GameObject instigator, int team)
//     {
//         //throttle attack using cooldown
//         float nextAttackTime = _lastAttackTime + 1f / AttackRate;
//         if(Time.time> nextAttackTime)
//         {
//             _lastAttackTime = Time.time;
//             Attack(aimPostion, instigator, team);
//             return true;
//         }
//         return false;

//     }


//     protected virtual void Attack(Vector3 aimPostion, GameObject instigator, int team)
//     {
//         //PLAY FMOD AUDIO 
//         if (!AttackSFX.IsNull) RuntimeManager.PlayOneShot(AttackSFX, transform.position);
//     }
// }
