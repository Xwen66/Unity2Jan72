// using UnityEngine;

// public class WeaponMelee : Weapon
// {
//     [SerializeField] private float _angle = 120f;
//     [SerializeField] private LayerMask _hitMask;

//     protected override void Attack(Vector3 aimPostion, GameObject instigator, int team)
//     {
//         base.Attack(aimPostion, instigator, team);
//         Vector3 origin = instigator.transform.position;
//         Vector3 aimDirection = (aimPostion - origin).normalized;

//         // WE'RE USING AN OVERLAPSHERE NOT A SPHERECAST
//         //OVERLAPSHERE IS STATIONARY, SPHERECAST MOVES A SPHERE THROUGH SPACE

//         Collider[] hits = Physics.OverlapSphere(origin, Range, _hitMask);
//         foreach (Collider hit in hits)
//         {

//             //don't punch self in face
//             {
//                 if (hit.gameObject == instigator) continue; //continue skips to next iteration

//                 Vector3 targetDir = (hit.transform.position - origin).normalized;
//                 float angleToHit = Vector3.Angle(targetDir, aimDirection);
//                 if (angleToHit > _angle) continue;

//                 //damage the target 
//                 if(hit.TryGetComponent(out Health targetHealth))
//                 {
//                     targetHealth.Damage(new DamageInfo(Damage, hit.gameObject, gameObject,instigator, DamageType));
//                 }




//             }

//         }
//     }
// }
