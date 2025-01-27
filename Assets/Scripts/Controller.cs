using UnityEngine;
using Sirenix.OdinInspector;

//[RequireComponent] force the named component onto the smae GameObject
[RequireComponent (typeof(CustomerCharacterMovement))]

public class Controller : MonoBehaviour
{
    //properties, we can add custom access modifiers
    //or even code execution to a value when we get / set it
    //[field: SerializeField] exposes the 'backing field' instead
    [field: SerializeField] public CustomerCharacterMovement Movement {  get; private set; } //get / private set is effectively read only

    //OnValidate is an EDITOR ONLY FUNCTION - it gets called when inspector values are loaded / modified
    //it also gets called when a component is first added
    // if we don't serialize values set in OnValidate your script will break in builds

    //inline buttons appear alongside the value in the inspector
    [field: SerializeField, InlineButton(nameof(FindWeapons), "Find")] public Weapon[] Weapons { get; private set; }

    protected virtual void OnValidate()
    {
        Movement = GetComponent<CustomerCharacterMovement>();

    }
    private void FindWeapons()
    {
        Weapons = GetComponentsInChildren<Weapon>();
    }

}
