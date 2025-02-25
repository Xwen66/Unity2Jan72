using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionQuit : MonoBehaviour
{
    private void Start()
    {
        if(TryGetComponent(out Button button))
        {
            button.onClick.AddListener(Quit);
        }
    }

    private void Quit()
    {
        // quit in build
        Application.Quit();

        // quit in editor
        // #if conditionally compiles code, in this case, only in the editor
        // we don't need this code in a build so it gets stripped out automatically
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}