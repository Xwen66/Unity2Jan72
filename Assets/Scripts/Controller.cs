using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UIElements.Experimental;

//[RequireComponent] force the named component onto the smae GameObject
[RequireComponent (typeof(CustomerCharacterMovement))]
[RequireComponent (typeof(Targetable))]
[RequireComponent (typeof(Health))]
[RequireComponent (typeof(Vision))]

public class Controller : MonoBehaviour
{
    //properties, we can add custom access modifiers
    //or even code execution to a value when we get / set it
    //[field: SerializeField] exposes the 'backing field' instead
    [field: SerializeField] public CustomerCharacterMovement Movement {  get; private set; } //get / private set is effectively read only
    [field: SerializeField] public Targetable Targetable { get; private set; }
    [field: SerializeField] public Health Health{ get; private set; }
    [field: SerializeField] public Vision Vision{ get; private set; }

    //OnValidate is an EDITOR ONLY FUNCTION - it gets called when inspector values are loaded / modified
    //it also gets called when a component is first added
    // if we don't serialize values set in OnValidate your script will break in builds

    //inline buttons appear alongside the value in the inspector
    [field: SerializeField, InlineButton(nameof(FindWeapons), "Find")] public Weapon[] Weapons { get; private set; }
    protected virtual void OnValidate()
    {
       if(Movement==null) Movement = GetComponent<CustomerCharacterMovement>();
       if(Targetable==null) Targetable = GetComponent<Targetable>();
       if(Health==null) Health = GetComponent<Health>();
       if(Vision==null) Vision = GetComponent<Vision>();
    }
    private void FindWeapons()
    {
        Weapons = GetComponentsInChildren<Weapon>();
    }

}
