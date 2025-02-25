using System.Collections;
using UnityEngine;

public class EnemyController : Controller
{
    [SerializeField] private Targetable _target;
    [SerializeField] private float _waypointReachedDistance = 1f;
    [SerializeField] private float _wanderSpeedMultiplier = 0.5f;

    // auto-use first equipped weapon
    private Weapon Weapon => Weapons[0];

    // target properties
    public bool IsTargetValid => _target != null && _target.IsTargetable;
    public bool IsTargetVisible => Vision.TestVisibility(_target.transform.position);
    public float TargetDistance => Vector3.Distance(_target.transform.position, transform.position);
    public Vector3 TargetPosition => _target.transform.position;

    private Waypoint[] _waypoints;
    private IEnumerator _currentState;

    private void Start()
    {
        // find all waypoints in scene(s)
        _waypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);

        // add listener to death event
        // Health.OnDeath.AddListener( (DamageInfo info) => { ChangeState(DeadState()); } );  this example is a lamda expression to change the state
        Health.OnDeath.AddListener(Death);
        Health.OnDamage.AddListener(Damaged);

        ChangeState(WanderState());
    }

    private void Damaged(DamageInfo damageInfo)
    {
        // if valid target on opposing team, set as target
        if(damageInfo.Instigator.TryGetComponent(out Targetable attacker) && attacker.Team != Targetable.Team)
        {
            // enemy damage us
            _target = attacker;
        }
    }

    private void Death(DamageInfo damageInfo)
    {
        ChangeState(DeadState());
    }

    private void ChangeState(IEnumerator newState)
    {
        Movement.MoveSpeedMultiplier = 1f;

        // stop current state
        if (_currentState != null) StopCoroutine(_currentState);

        // assign and start new state
        _currentState = newState;
        StartCoroutine(_currentState);
    }

    private void TryFindTarget()
    {
        if (IsTargetValid) return;  // stop if we have target
        _target = Vision.GetFirstVisibleTarget(Targetable.Team);
    }

    private IEnumerator WanderState()
    {
        Waypoint waypoint = null;
        Movement.MoveSpeedMultiplier = _wanderSpeedMultiplier;

        // wander until target is found
        while (!IsTargetValid)
        {
            TryFindTarget();

            // find new waypoint if invalid or current reached
            if (waypoint == null || Vector3.Distance(waypoint.Position, transform.position) < _waypointReachedDistance)
            {
                int waypointIndex = Random.Range(0, _waypoints.Length);
                waypoint = _waypoints[waypointIndex];
            }

            Movement.MoveTo(waypoint.Position);

            yield return null;  // yield return null tells the coroutine to pause and start here again next frame
        }

        ChangeState(AttackState());
    }

    private IEnumerator AttackState()
    {
        // chase and attack target
        while(IsTargetValid)
        {
            // chase target
            if(TargetDistance > Weapon.EffectiveRange || !IsTargetVisible)
            {
                Movement.MoveTo(TargetPosition);
                Debug.DrawLine(transform.position, TargetPosition, Color.yellow);
            }
            else  // attack target
            {
                Movement.Stop();
                Movement.SetLookPosition(TargetPosition);
                Debug.DrawLine(transform.position, TargetPosition, Color.red);

                // attempt attack with weapon
                if(Weapon.TryAttack(TargetPosition, gameObject, Targetable.Team))
                {
                    // if successful, wait for duration of attack
                    yield return new WaitForSeconds(Weapon.Duration);
                }
            }

            // wait for next frame
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