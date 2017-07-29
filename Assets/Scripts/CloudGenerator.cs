using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{

    private GameObject clouds;
    private float timer;

    public void Awake()
    {
        clouds = (GameObject)Resources.Load("Prefabs/Cloud");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            int rnd = Random.Range(0, 10);
            if (rnd >= 7)
            {
                Instantiate(clouds, this.transform.position, Quaternion.identity);
            }
            timer = 0;
        }
    }
}
