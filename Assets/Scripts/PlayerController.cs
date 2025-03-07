using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : Controller
{
    // LayerMask is used to filter Raycast checks
    [SerializeField] private LayerMask _groundMask;

    private Vector2 _moveInput2D;
    private Vector3 _aimPosition;

    // called by PlayerInput component, InputValue contains input type configured in InputActions
    public void OnMove(InputValue inputValue)
    {
        // Get<> reads input types from player input
        _moveInput2D = inputValue.Get<Vector2>();
    }

    // InputValue parameters are optional, we don't care about them for jump
    public void OnJump()
    {
        if(!Health.IsAlive) return;

        Movement.TryJump();
    }

    public void OnTeleport()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, _groundMask))
        {
            Vector3 teleportLocation = hitInfo.point;
            Movement.Teleport(teleportLocation);
        }
    }

    public void OnShoot()
    {
        if (!Health.IsAlive) return;

        // assume shotgun is weapon 0
        Weapon shotgun = Weapons[0];
        shotgun.TryAttack(_aimPosition, gameObject, 0);
    }

    private void Update()
    {
        // stop actions if dead
        if(!Health.IsAlive)
        {
            Movement.Stop();
            return;
        }

        // map 2D input to 3D world space direction
        Vector3 right = Camera.main.transform.right;        // thumb
        Vector3 up = Vector3.up;                            // pointer finger
        Vector3 forward = Vector3.Cross(right, up);
        Vector3 moveInput3D = right * _moveInput2D.x + forward * _moveInput2D.y;
        Debug.DrawRay(transform.position, moveInput3D, Color.magenta);

        // send move input to movement component
        Movement.SetMoveInput(moveInput3D);

        // find ray travelling through mouse into world
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, _groundMask))
        {
            Vector3 hitPoint = hitInfo.point;
            Debug.DrawRay(hitPoint, Vector3.up, Color.magenta);

            // make character look at hit point
            Movement.SetLookPosition(hitPoint);

            // find aim position
            _aimPosition = hitPoint + new Vector3(0f, 1f, 0f);
        }
    }
}