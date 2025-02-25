using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionEnableGameObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] _toEnable;
    [SerializeField] private GameObject[] _toDisable;

    private void Start()
    {
        if(TryGetComponent(out Button button))
        {
            button.onClick.AddListener(EnableGameObjects);
        }
    }

    private void EnableGameObjects()
    {
        foreach (GameObject go in _toEnable)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in _toDisable)
        {
            go.SetActive(false);
        }
    }
}