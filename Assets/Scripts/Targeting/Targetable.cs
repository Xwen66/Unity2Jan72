using UnityEngine;

public class Targetable : MonoBehaviour
{
    [field: SerializeField] public int Team { get; set; } = 1;
    [field: SerializeField] public bool IsTargetable { get; private set; } = true;



}
