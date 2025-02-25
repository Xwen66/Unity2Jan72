using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _spawnPosition;
    private float _range;
    private float _damage;
    private DamageType _damageType;
    private GameObject _instigator;
    private int _team;

    private void OnValidate()
    {
        if(_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;   // best collision mode for fast moving objects
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;                  // update visuals in between physics ticks
        }

        // try get collider
        if(TryGetComponent(out CapsuleCollider collider))
        {
            collider.isTrigger = true;
        }
    }

    public void Launch(float projectileSpeed, float range, float damage, DamageType damageType, GameObject instigator, int team)
    {
        _spawnPosition = transform.position;
        _rigidbody.linearVelocity = transform.forward * projectileSpeed;
        _range = range;
        _damage = damage;
        _damageType = damageType;
        _instigator = instigator;
        _team = team;
    }

    private void Update()
    {
        // clean up if travelled past max distance
        float distanceTravelled = Vector3.Distance(transform.position, _spawnPosition);
        if(distanceTravelled > _range)
        {
            Cleanup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ignore shooting self
        if (other.gameObject == _instigator) return;

        // otherwise attempt to damage object hit
        if (other.TryGetComponent(out Health targetHealth))
        {
            DamageInfo damageInfo = new DamageInfo(_damage, other.gameObject, gameObject, _instigator, _damageType);
            targetHealth.Damage(damageInfo);
        }

        Cleanup();
    }

    private void Cleanup()
    {
        Destroy(gameObject);

        // TODO: handle VFX, SFX etc.
    }
}
