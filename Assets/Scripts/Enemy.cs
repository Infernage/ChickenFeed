﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 30;

    private int marginDistance = 50;

    private CameraController _cameraController;
    private Vector3 initialPosition;

    private bool enableScreenShake = false;

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

        if (enableScreenShake)
        {
            _cameraController.shake = 0.5F;
            _cameraController.shakeAmount =
                ((marginDistance - (transform.position - initialPosition).magnitude) / marginDistance) * 0.3F;
        } else
        {
            _cameraController.shake = 0;
        }
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

        initialPosition = transform.position;

        transform.position =
            transform.position +
            (transform.up * -marginDistance);

        enableScreenShake = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyAttackAliveArea = collision.gameObject.GetComponent<EnemyAttackAliveArea>();
        if (enemyAttackAliveArea != null)
        {
            enableScreenShake = false;
        }
    }
}
