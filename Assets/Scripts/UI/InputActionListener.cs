using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputActionListener : MonoBehaviour
{
    [SerializeField] private InputActionReference _actionReference;
    [SerializeField] private Button _activateButton;

    public UnityEvent OnInput;

    private void OnEnable()
    {
        // unity's input systm uses C# events by default
        // they work almost identical to UnityEvents, but the syntax is bit different
        _actionReference.action.performed += Performed;
    }

    private void OnDisable()
    {
        _actionReference.action.performed -= Performed;
    }

    private void Performed(InputAction.CallbackContext context)
    {
        OnInput.Invoke();
        _activateButton?.onClick.Invoke();  // ?. is an inline null check, if _activateButton is null, it doesn't execute the function
    }

    // allow us to manually invoke event without any input
    public void ForceActivate()
    {
        OnInput.Invoke();
    }
}