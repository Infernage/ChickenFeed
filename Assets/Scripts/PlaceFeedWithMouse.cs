using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFeedWithMouse : MonoBehaviour {

    public float surfaceOffset = 1.5f;

    public PiensoManager feedManager;

    // Use this for initialization
    void Start () {

        feedManager = gameObject.AddComponent<PiensoManager>();


		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider == null)
        {
            return;
        }

        Debug.Log("Click coords:" + hit.point + hit.normal);


        feedManager.AddPienso(hit.point + hit.normal);
        

    }
}
