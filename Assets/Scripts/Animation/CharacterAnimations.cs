using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CustomerCharacterMovement _characterMovement;

    private void OnValidate()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_characterMovement == null) _characterMovement = GetComponent<CustomerCharacterMovement>();
    }

    private void Update()
    {
        Vector3 worldVelocity = _characterMovement.Velocity;
        Vector3 localVelocity = transform.InverseTransformVector(worldVelocity);
        localVelocity /= _characterMovement.Speed;

        // set animator values
        _animator.SetFloat("Forward", localVelocity.z);
        _animator.SetFloat("Right", localVelocity.x);
        _animator.SetBool("IsGround", _characterMovement.IsGrounded);
    }
}