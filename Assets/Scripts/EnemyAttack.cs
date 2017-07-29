using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Enemy enemy;
    public GameObject trackGameObject;

    private BoxCollider2D _enemySpawnArea;
    private EnemyAttackAliveArea _enemyAttackAliveArea;

    void Start()
    {
        trackGameObject.SetActive(false);
    }

    // Use this for initialization
    void Awake () {
        _enemySpawnArea = GetComponentInChildren<EnemyAttackSpawnArea>().gameObject.GetComponent<BoxCollider2D>();
        _enemyAttackAliveArea = GetComponentInChildren<EnemyAttackAliveArea>();
        enemy = GetComponentInChildren<Enemy>();

        _enemyAttackAliveArea.SetEnemyAttack(this);
    }

    Vector2 GetRandomPoint()
    {
        var randomPoint = Random.insideUnitCircle;
        return new Vector2(
            (_enemySpawnArea.bounds.size.x / 2) * randomPoint.x,
            (_enemySpawnArea.bounds.size.y / 2) * randomPoint.y);
    }

    Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public void Run()
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

    public void Stop()
    {
        trackGameObject.SetActive(false);
        enemy.SetActive(false);
    }
}
