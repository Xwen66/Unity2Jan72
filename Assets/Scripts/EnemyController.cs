using System.Collections;
using UnityEngine;

public class EnemyController : Controller
{
    [SerializeField] private Targetable _target;
    [SerializeField] private float _waypointReachedDistance = 1f;
    [SerializeField] private float _wanderSpeedMultiplier = 0.5f;

    //auto-use first equipped weapon
    private Weapon Weapon => Weapons[0];

    //target properties
    public bool IsTargetValid => _target != null && _target.IsTargetable;
    public bool IsTargetable => Vision.TestVisibility(_target.transform.position); 
    public float TargetDistance => Vector3.Distance(_target.transform.position, transform.position);
    public Vector3 TargetPosition => _target.transform.position;

    private Waypoint[] _waypoints;
    private IEnumerator _currentState;
    private void Start()
    {
        //find all waypoints in scene(s)
        _waypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        //add listener to death event
        //Health.OnDeath.AddListener((DamageInfo info) => { ChangeState(DeadState())});  example of lambda expression

        Health.OnDeath.AddListener(Death); 

        ChangeState(WanderState());

    }

    private void Death(DamageInfo damageInfo)
    {
        ChangeState(DeadState());
    }   

    private void TryFindTarget()
    {
        if (IsTargetValid) return;
        _target = Vision.GetFirstVisibleTarget(Targetable.Team);

    }
    private void ChangeState(IEnumerator newState)
    {
        if (_currentState != null)
        {
            StopCoroutine(_currentState);
        }
        _currentState = newState;
        StartCoroutine(_currentState);
    }
    private IEnumerator WanderState()
    {

        Waypoint waypoint = null;
        Movement.MoveSpeedMultiplier = _wanderSpeedMultiplier;

        //wander until target is found 
        while (!IsTargetValid)
        {
            TryFindTarget();

            if (waypoint ==null || Vector3.Distance(waypoint.Position, transform.position) < _waypointReachedDistance)
            {
                int waypointIndex = Random.Range(0, _waypoints.Length);
                waypoint = _waypoints[waypointIndex];
            }
            Movement.MoveTo(waypoint.Position);
            yield return null; //yield return null waits for the next frame
        }
        ChangeState(AttackState());
    }

    private IEnumerator AttackState()
    {
        Movement.MoveSpeedMultiplier = 1f;
        while (IsTargetValid)
        {
            //chase target
            if (TargetDistance > Weapon.EffectiveRange || !IsTargetValid)
            {
                Movement.MoveTo(TargetPosition);
                Debug.DrawLine(transform.position, TargetPosition, Color.red);
            }
            else //attack target
            {
                Movement.Stop();
                Movement.SetLookPosition(TargetPosition);
                Debug.DrawLine(transform.position, TargetPosition, Color.green);

                //attempt attack with weapon
                if(Weapon.TryAttack(TargetPosition, gameObject, Targetable.Team))
                {
                    //if successful, wait for duration of attack 
                    yield return new WaitForSeconds(Weapon.Duration);
                }
            }
            //wait for next frame
            yield return null;
        }
        ChangeState(WanderState());
    }

    private IEnumerator DeadState()
    {
        Movement.Stop();
        yield return null;
    }
}
