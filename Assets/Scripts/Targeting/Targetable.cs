using UnityEngine;

public class Targetable : MonoBehaviour
{
    [field: SerializeField] public int Team { get; private set; } = 1;  // Quin's preference - enemies on team 1, player on team 0
    [field: SerializeField] public bool IsTargetable { get; private set; } = true;

}