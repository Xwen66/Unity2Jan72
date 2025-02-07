using UnityEngine;

public class EnemyController : Controller
{
    [SerializeField] private Transform _target;


    //auto-use first equipped weapon
    private Weapon Weapon => Weapons[0];
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(_target == null) return;

        //distance to gatget 
        float targetDistance = Vector3.Distance(transform.position, _target.position);
        if(targetDistance > Weapon.EffectiveRange)
        {
            Movement.MoveTo(_target.position);
        }
        else
        {
            Movement.Stop();
            Movement.SetLookPosition(_target.position);
            Weapon.TryAttack(_target.position, gameObject, 1);
        }



    }
}
