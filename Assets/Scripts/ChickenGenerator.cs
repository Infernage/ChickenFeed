using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGenerator : MonoBehaviour {
    public int numberOfChickens = 0;
    public float x, y;
    public GameObject ChickenPrefab;

	// Use this for initialization
	void Start () {
        if (numberOfChickens == 0) numberOfChickens = 20;

        for (int i = 0; i < numberOfChickens; i++)
        {
            Instantiate(ChickenPrefab, new Vector3(Random.Range(-x, x), Random.Range(-y, y)), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
