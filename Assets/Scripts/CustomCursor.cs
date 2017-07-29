using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{

    private Texture2D handClosed, handOpen;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    public void Awake()
    {
        handClosed = (Texture2D)Resources.Load("Cursor/handClosed");
        handOpen = (Texture2D)Resources.Load("Cursor/handOpen");
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(handClosed, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(handOpen, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(handClosed, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(handOpen, Vector2.zero, cursorMode);
    }
}
