using UnityEngine;

public class EnemyController : Controller
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stoppingDistance = 2f;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(_target == null) return;

        //distance to gatget 
        float targetDistance = Vector3.Distance(transform.position, _target.position);
        if(targetDistance > _stoppingDistance)
        {
            Movement.MoveTo(_target.position);
        }
        else
        {
            Movement.Stop();
            Movement.SetLookPosition(_target.position);
        }



    }
}
