using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAliveArea : MonoBehaviour {

    private EnemyAttack _enemyAttack;

    public void SetEnemyAttack(EnemyAttack enemyAttack)
    {
        _enemyAttack = enemyAttack;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            _enemyAttack.Stop();
        }
    }
}
