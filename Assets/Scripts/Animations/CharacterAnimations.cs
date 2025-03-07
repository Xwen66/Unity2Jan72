using System;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CustomCharacterMovement _characterMovement;

    private void OnValidate()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        if(_characterMovement == null) _characterMovement = GetComponent<CustomCharacterMovement>();
    }

    private void Start()
    {
        // add listener to health OnDeath event
        if(TryGetComponent(out Health health))
        {
            health.OnDeath.AddListener(Death);
        }
    }

    private void Death(DamageInfo damageInfo)
    {
        // set IsAlive bool
        _animator.SetBool("IsAlive", false);
    }

    private void Update()
    {
        Vector3 worldVelocity = _characterMovement.Velocity;
        Vector3 localVelocity = transform.InverseTransformVector(worldVelocity);
        localVelocity /= _characterMovement.Speed;

        // set animator values
        _animator.SetFloat("Forward", localVelocity.z);
        _animator.SetFloat("Right", localVelocity.x);
        _animator.SetBool("IsGrounded", _characterMovement.IsGrounded);
    }
}