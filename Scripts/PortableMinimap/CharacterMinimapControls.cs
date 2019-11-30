using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMinimapControls : MonoBehaviour
{
    public KeyCode minimapKey;
    public MiniMapHandler miniMapHandler;

    void Update()
    {
        if (Input.GetKeyDown(minimapKey))
        {
            MinimapAction();
        }
    }

    private void MinimapAction()
    {
        if (miniMapHandler != null)
        {
            miniMapHandler.UseMinimap();
        }
    }
}
