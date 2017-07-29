using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Enemy enemy;
    public GameObject trackGameObject;

    private BoxCollider2D _boxCollider2d;

    void Start()
    {
        trackGameObject.SetActive(false);
    }

    // Use this for initialization
    void Awake () {
        _boxCollider2d = GetComponent<BoxCollider2D>();
        enemy = GetComponentInChildren<Enemy>();
	}

    Vector2 GetRandomPoint()
    {
        var randomPoint = Random.insideUnitCircle;
        return new Vector2(
            (_boxCollider2d.bounds.size.x / 2) * randomPoint.x,
            (_boxCollider2d.bounds.size.y / 2) * randomPoint.y);
    }

    Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public void Calc()
    {
        trackGameObject.SetActive(true);

        var randomPoint = GetRandomPoint();
        var randomRotation = GetRandomRotation();

        enemy.SetPosition(randomPoint);
        enemy.SetRotation(randomRotation);

        trackGameObject.transform.position = randomPoint;
        trackGameObject.transform.rotation = randomRotation;

        enemy.Run();
    }
}
