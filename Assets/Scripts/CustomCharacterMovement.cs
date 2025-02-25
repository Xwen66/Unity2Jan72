using UnityEngine;
using CharacterMovement;

public class CustomCharacterMovement : CharacterMovement3D
{
    public void Teleport(Vector3 position)
    {
        transform.position = position;
        // depending on timing, transform and rigidbody can get out of sync, so we do both
        Rigidbody.position = position;
    }
}