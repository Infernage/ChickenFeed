﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 30;

    private CameraController _cameraController;

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    void Update () {
        transform.position =
            transform.position +
            (transform.up * speed * Time.deltaTime);

        _cameraController.shake = 0.5F;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Run()
    {
        SetActive(true);

        transform.position =
            transform.position +
            (transform.up * -50);
    }
}
