using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private int speed = 2;
    private Vector3 vector;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int rnd = Random.Range(1, 2);
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/nube" + rnd);
        int randomVec = Random.Range(0, 3);
        switch (randomVec)
        {
            case 0:
                vector = new Vector3(1, 0, 0);
                break;
            case 1: vector = new Vector3(-1, 0, 0); break;
            case 2: vector = new Vector3(0, 1, 0); break;
            case 3: vector = new Vector3(0, -1, 0); break;
        }
    }

    void Start()
    {
        Invoke("CloudDestroy", 20);
    }

    private void CloudDestroy()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(vector * Time.deltaTime * speed);
    }
}
