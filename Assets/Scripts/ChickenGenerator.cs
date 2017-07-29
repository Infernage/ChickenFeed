using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGenerator : MonoBehaviour {
    public int numberOfChickens = 0;
    public float x, y;
    public GameObject ChickenPrefab;

    private List<GameObject> chickens;

	// Use this for initialization
	void Start () {
        if (numberOfChickens == 0) numberOfChickens = 20;

        chickens = new List<GameObject>();

        for (int i = 0; i < numberOfChickens; i++)
        {
            GameObject chicken = Instantiate(ChickenPrefab, new Vector3(Random.Range(-x, x), Random.Range(-y, y)), Quaternion.identity);
            ChickenAI ai = chicken.GetComponent<ChickenAI>();
            ai.destroyed += Ai_destroyed;
            chickens.Add(chicken);
        }
	}

    private void Ai_destroyed(object sender, System.EventArgs e)
    {
        chickens.Remove(sender as GameObject);

        if (chickens.Count == 0)
        {
            // TODO: Load game over
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
