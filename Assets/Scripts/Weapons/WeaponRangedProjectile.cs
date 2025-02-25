using UnityEngine;
using Sirenix.OdinInspector;

public class WeaponRangedProjectile : Weapon
{
    [SerializeField, BoxGroup("Ranged Projectile")] private Projectile _bulletPrefab;
    [SerializeField, BoxGroup("Ranged Projectile")] private Transform _muzzle;
    [SerializeField, BoxGroup("Ranged Projectile")] private float _inaccuracy = 10f;
    [SerializeField, BoxGroup("Ranged Projectile")] private float _projectileSpeed = 30f;
    [SerializeField, BoxGroup("Ranged Projectile")] private int _shotCount = 6;

    protected override void Attack(Vector3 aimPosition, GameObject instigator, int team)
    {
        base.Attack(aimPosition, instigator, team);

        Debug.DrawLine(_muzzle.position, aimPosition, Color.red, 1f);

        Vector3 spawnPos = _muzzle.position;
        Vector3 aimDir = (aimPosition - spawnPos).normalized;       // direction from A to B, is B minus A, normalized
        Quaternion spawnRot = Quaternion.LookRotation(aimDir);      // LookRotation() turns a DIRECTION into a ROTATION

        for (int i = 0; i < _shotCount; i++)
        {
            // randomly generate inaccuracy
            float inaccX = Random.Range(-_inaccuracy, _inaccuracy);
            float inaccY = Random.Range(-_inaccuracy, _inaccuracy);

            // create rotation from inaccuracy (more Quaternion fun)
            Vector3 leftRightAngle = _muzzle.up * inaccX;
            Vector3 upDownAngle = _muzzle.right * inaccY;
            Quaternion inaccRotation = Quaternion.Euler(leftRightAngle + upDownAngle);  // Euler() turns ueler angles (x,y,z) into a ROTATION

            // combine both spawn rotation and inaccuracy rotation
            Quaternion finalRotation = spawnRot * inaccRotation;    // we multiply quaternions to add their rotations

            // draw debug line for each bullet
            Vector3 randomizedDir = finalRotation * Vector3.forward;    // multiplying a ROTATION by a DIRECTION results in the direction from that rotation
            Debug.DrawRay(spawnPos, randomizedDir * Range, Color.red, 0.5f);

            // spawn projectile and assign values
            Projectile spawnedProjectile = Instantiate(_bulletPrefab, spawnPos, finalRotation);
            spawnedProjectile.Launch(_projectileSpeed, Range, Damage, DamageType, instigator, team);
        }
    }
}