using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public EnemyAttack attack;
    public float maxTime;

	// Use this for initialization
	void Start () {
        Invoke("Spawn", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
        CancelInvoke();

        attack.Run();
        Invoke("Spawn", Random.Range(0f, maxTime));
    }
}
