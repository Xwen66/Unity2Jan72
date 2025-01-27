using UnityEngine;
using CharacterMovement;
using UnityEngine.UIElements;
public class CustomerCharacterMovement : CharacterMovement3D
{
    private int _canJumpRemain;

    [field: SerializeField] public int CanJumpTime { get; set; } = 2;

    private void Start()
    {
        _canJumpRemain = CanJumpTime;
    }

    public void Teleport(Vector3 location)
    {
        transform.position = location;
        //dependin on timing, transform and rigidbody can get out of sync, so we do both
        Rigidbody.position = location;
    }

    public override void TryJump()
    {
        if (CanJumpTime <= 0) return;
        if (base.CanMove && base.CanCoyoteJump)
        {
            Jump();
            _canJumpRemain -= 1;
        }
    }

    protected override bool CheckGrounded()
    {
        RaycastHit hitInfo;
        bool flag = Physics.Raycast(GroundCheckStart, -base.transform.up, out hitInfo, base.GroundCheckDistance, base.GroundMask);
        base.GroundNormal = Vector3.up;
        base.SurfaceVelocity = Vector3.zero;
        if (!flag)
        {
            return false;
        }

        if (hitInfo.rigidbody != null)
        {
            base.SurfaceVelocity = hitInfo.rigidbody.linearVelocity;
        }

        if (Vector3.Angle(base.transform.up, hitInfo.normal) < base.MaxSlopeAngle)
        {
            base.LastGroundedTime = Time.timeSinceLevelLoad;
            base.GroundNormal = hitInfo.normal;
            base.LastGroundedPosition = base.transform.position;
            base.SurfaceObject = hitInfo.collider.gameObject;
            if (base.ParentToSurface)
            {
                base.transform.SetParent(base.SurfaceObject.transform);
            }
            _canJumpRemain = CanJumpTime;
            return true;
        }

        base.SurfaceObject = null;
        if (base.ParentToSurface)
        {
            base.transform.SetParent(null);
        }

        return false;
    }
}
