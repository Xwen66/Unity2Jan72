using UnityEngine;
using CharacterMovement;
using UnityEngine.UIElements;
public class CustomerCharacterMovement : CharacterMovement3D
{
    public void Teleport(Vector3 location)
    {
        transform.position = location;
        //dependin on timing, transform and rigidbody can get out of sync, so we do both
        Rigidbody.position = location;
    }
}
