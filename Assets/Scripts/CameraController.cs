using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera _camera;

    public float shake = 0;
    public float shakeAmount = 0.7F;
    public float decreaseFactor = 1.0F;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shake > 0)
        {
            _camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            _camera.transform.localPosition = new Vector3();
            shake = 0;
        }
    }
}
