using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFeedWithMouse : MonoBehaviour {

    public float surfaceOffset = 1.5f;

    public PiensoManager feedManager;

    Texture2D HandClosed;
    Texture2D HandOpen;

    // Use this for initialization
    void Start () {

        feedManager = gameObject.AddComponent<PiensoManager>();
        
        HandClosed = Resources.Load("Sprites/Hand_Closed") as Texture2D;
        HandOpen = Resources.Load("Sprites/Hand_Open") as Texture2D;

    }
	
	// Update is called once per frame
	void Update () {

        if (!Input.GetMouseButton(0))
        {
            Cursor.SetCursor(HandClosed, Vector2.zero, CursorMode.Auto);
            return;
        }
        Cursor.SetCursor(HandOpen, Vector2.zero, CursorMode.Auto);

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        Debug.Log("Click coords:" + hit.point + hit.normal);


        feedManager.AddPienso(hit.point + hit.normal);

        

    }
}
