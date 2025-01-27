using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fillBar;

    private void OnValidate()
    {
        if(_health == null) _health = GetComponentInParent<Health>();


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_health == null) return;
        _fillBar.fillAmount = _health.Percentage;

        //make health bar face camera
        transform.rotation = Camera.main.transform.rotation;

    }
}
