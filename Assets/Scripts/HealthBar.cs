using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fillBar;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnValidate()
    {
        if(_health == null) _health = GetComponentInParent<Health>();
        if(_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (_health == null || !_health.IsAlive || _health.Percentage >= 1f)
        {
            _canvasGroup.alpha = 0f;
            return;
        }

        _canvasGroup.alpha = 1f;

        // fill bar using "lazy" polling method, we'll fix this later
        _fillBar.fillAmount = _health.Percentage;

        // make health bar face camera
        transform.rotation = Camera.main.transform.rotation;
    }
}