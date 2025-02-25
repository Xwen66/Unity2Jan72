using UnityEngine;
using FMOD;
using FMODUnity;

// tell compiler to use Unity Debug class, not FMOD
using Debug = UnityEngine.Debug;

public class SetFMODValue : SetSliderValue
{
    [SerializeField] private string _parameterName = "Volume";

    protected override void SetValue(float value)
    {
        base.SetValue(value);

        float remappedValue = SliderValueToOutputValue(value);
        // set global FMOD parameter
        RESULT result = RuntimeManager.StudioSystem.setParameterByName(_parameterName, remappedValue);
        if(result != RESULT.OK)
        {
            Debug.LogWarning($"FMOD parameter {_parameterName} set fail: {result}");
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        RESULT result = RuntimeManager.StudioSystem.getParameterByName(_parameterName, out float value);
        if(result == RESULT.OK)
        {
            float sliderValue = OutputValueToSliderValue(value);
            _slider.SetValueWithoutNotify(sliderValue);
        }
    }
}