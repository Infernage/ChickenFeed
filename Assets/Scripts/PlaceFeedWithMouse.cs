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
