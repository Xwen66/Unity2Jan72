// using UnityEngine;
// using Sirenix.OdinInspector;
// public class WeaponRangedProjectile : Weapon
// {
//     [SerializeField, BoxGroup("Ranged Prjectile")] private Projectile _bulletPrefab;
//     [SerializeField, BoxGroup("Ranged Prjectile")] private float _inaccuracy = 10f;
//     [SerializeField, BoxGroup("Ranged Prjectile")] private Transform _muzzle;
//     [SerializeField, BoxGroup("Ranged Prjectile")] private float _projectileSpeed;
//     [SerializeField, BoxGroup("Ranged Prjectile")] private int _shotCount = 6;

//     protected override void Attack(Vector3 aimPostion, GameObject instigator, int team)
//     {
//         base.Attack(aimPostion, instigator, team);

//         Debug.DrawLine(_muzzle.position, aimPostion, Color.red, 1f);
       
//         Vector3 spawnPos = _muzzle.position;                      
//         Vector3 aimDir = (aimPostion - spawnPos).normalized;
//         Quaternion spawnRot = Quaternion.LookRotation(aimDir); //lookrotation() turns a direction into a rotation

//         for (int i = 0; i < _shotCount; i++)
//         {
//             //randomly generate inaccuracy 
//             float inaccX = Random.Range(-_inaccuracy, _inaccuracy);
//             float inaccY = Random.Range(-_inaccuracy, _inaccuracy);

//             //create rotation from inaccuracy (more Quaternion)
//             Vector3 leftRighAngle = _muzzle.up * inaccX;
//             Vector3 upDownAngle = _muzzle.right * inaccY;
//             Quaternion inaccRotation = Quaternion.Euler(leftRighAngle + upDownAngle);  //Euler() turns ueler angles (x,y,z) into a ROTATION

//             //combine both spawn rotation and inaccuracy rotation 
//             Quaternion finalRotation = spawnRot * inaccRotation; //we multiply quaternions to add their rotations

//             //draw debug line for each bullet
//             Vector3 randomizedDir = finalRotation * Vector3.forward; //multiplying a roation by a direction results in a direction from that rotation 
//             Debug.DrawRay(spawnPos, randomizedDir* Range, Color.red, 0.5f);

//             Projectile spawnedProjectile = Instantiate(_bulletPrefab, spawnPos, finalRotation);
//             spawnedProjectile.Launch(_projectileSpeed, Range, Damage, DamageType, instigator, team);
//         }
//     }

// }
