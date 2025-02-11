using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]

public class PlayerController : Controller
{
    //layerMask is used to filter Raycast checks
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _sprintMultipler= 2;
    [SerializeField] private float _dashDistance = 10;
    Ray mouseRay;
    Rigidbody _rb;
    private Vector2 _moveInput2D;
    private Vector3 _aimPosition;

    RaycastHit hitInfo;
    //called by PlayerInput component, InputValue contains input type configured in InputActions

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void OnMove(InputValue inputValue)
    {
        // Get<> reads input types from player input
         _moveInput2D = inputValue.Get<Vector2>();
    }

    public void OnJump()
    {
        Movement.TryJump();


    }
    public void OnTeleport()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, _groundMask))
        {
            Vector3 teleportLocation = hitInfo.point;
            Movement.Teleport(teleportLocation);
        }
    }

    public void OnSprint(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            Movement.MoveSpeedMultiplier = _sprintMultipler;
            return;
        }
        Movement.MoveSpeedMultiplier = 1;
    }
    public void OnShoot()
    {
        //assume shootgun is weapon 0
        Weapon shotgun = Weapons[0];
        shotgun.TryAttack(_aimPosition, gameObject, 0);
    }

    public void OnDash()
    {
        Debug.Log("ondash");
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, _groundMask))
        {
            Vector3 dashDirection = ( hitInfo.point -transform.position).normalized;  
            dashDirection.y = 0; 
            _rb.linearVelocity = dashDirection * _dashDistance;
        }
    }


    private void Update()
    {
        //map 2D input to 3D world space direction 
        Vector3 right = Camera.main.transform.right;     //thumb
        Vector3 up = Vector3.up;                        //point finger
        Vector3 forward = Vector3.Cross(right, up);

        Vector3 moveInput3D = right * _moveInput2D.x + forward * _moveInput2D.y;
        Debug.DrawRay(transform.position, moveInput3D, Color.magenta);

        //send move input to movement component
        Movement.SetMoveInput(moveInput3D);

        //find ray travelling through mouse into world
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, _groundMask))
        {
            Vector3 hitPoint = hitInfo.point;
            Debug.DrawRay(hitPoint, Vector3.up, Color.magenta);

            //make scharcter look at hit point 
            Movement.SetLookPosition(hitPoint);

            _aimPosition = hitInfo.point + new Vector3(0f, 1f, 0f);
        }

    }
}
