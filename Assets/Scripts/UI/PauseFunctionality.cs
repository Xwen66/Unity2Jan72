using UnityEngine;
using UnityEngine.InputSystem;

public class PauseFunctionality : MonoBehaviour
{
    [SerializeField] private string _gameplayActionMapName = "Player";
    [SerializeField] private string _pauseActionMapName = "UI";

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void SwitchActionMap(string mapName)
    {
        // find first PlayerInput, assuming singeplayer game
        // int multiplayer you would have to find all players
        PlayerInput playerInput = FindFirstObjectByType<PlayerInput>();
        playerInput?.SwitchCurrentActionMap(mapName);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        SwitchActionMap(_pauseActionMapName);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SwitchActionMap(_gameplayActionMapName);
    }
}
