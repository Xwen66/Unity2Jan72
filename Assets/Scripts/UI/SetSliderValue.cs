using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class SetSliderValue : MonoBehaviour
{
    [SerializeField] protected Vector2 _outMinMax = new Vector2(-0.5f, 0.5f);
    [SerializeField] protected Slider _slider;

    private void OnValidate()
    {
        if(_slider == null) _slider = GetComponent<Slider>();
    }

    protected virtual void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetValue);
    }

    protected virtual void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetValue);
    }

    protected virtual void SetValue(float value)
    {
        // we'll override this in our child classes
    }

    // take slider value (0 - 10) and remap it to usable value for setting (0 to 1 for FMOD, -0.5f to 0.5f for brightness)
    protected float SliderValueToOutputValue(float value)
    {
        float percentage = value / _slider.maxValue;
        float remappedValue = Mathf.Lerp(_outMinMax.x, _outMinMax.y, percentage);
        return remappedValue;
    }

    // remap incoming setting value to display correctly on slider
    protected float OutputValueToSliderValue(float value)
    {
        float percentage = Mathf.InverseLerp(_outMinMax.x, _outMinMax.y, value);
        float sliderValue = percentage * _slider.maxValue;
        return sliderValue;
    }
}