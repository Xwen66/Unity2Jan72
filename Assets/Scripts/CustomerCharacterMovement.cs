using UnityEngine;
using CharacterMovement;
using UnityEngine.UIElements;
public class CustomerCharacterMovement : CharacterMovement3D
{
    private int _jumpCounter;

    [field: SerializeField] public int TotalJump { get; set; } = 2;

    private void Start()
    {
        _jumpCounter = TotalJump;
    }

    public void Teleport(Vector3 location)
    {
        transform.position = location;
        //dependin on timing, transform and rigidbody can get out of sync, so we do both
        Rigidbody.position = location;
    }

    public override void TryJump()
    {
        if (_jumpCounter > TotalJump) return;
        if (base.CanMove && base.CanCoyoteJump)
        {
            Jump();
            
        }
    }
  
    public override void Jump()
    {
        // calculate jump velocity from jump height and gravity
        float jumpVelocity = Mathf.Sqrt(2f * -Gravity * JumpHeight);
        // override current y velocity but maintain x/z velocity
        Velocity = new Vector3(Velocity.x, jumpVelocity, Velocity.z);
        _jumpCounter++;
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
            _jumpCounter = TotalJump;
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
