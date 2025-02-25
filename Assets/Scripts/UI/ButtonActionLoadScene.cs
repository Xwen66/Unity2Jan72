using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActionLoadScene : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Scene Name";

    private void Start()
    {
        if(TryGetComponent(out Button button))
        {
            button.onClick.AddListener(LoadScene);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}