using System.Collections;
using System.Collections.Generic;
using TowerDefensHP;
using UnityEngine;

public class TargetField : MonoBehaviour
{
    MeshRenderer targetFieldRenderer;

    [SerializeField] Material targetFieldMaterial;
    [SerializeField] Material defaultMaterial;

    [SerializeField] private TargetFieldPosition targetFieldPosition;

    private List<GameObject> enemiesOnTarget;
    public List<GameObject> EnemiesOnTarget => enemiesOnTarget;

    public TargetFieldPosition TargetFieldPosition => targetFieldPosition;

    private void Start()
    {
        targetFieldRenderer = GetComponent<MeshRenderer>();
        enemiesOnTarget = new List<GameObject>();
    }
    public void OnSensorTrigger(Sensor sensor)
    {
        if (sensor.IsActive)
        {
            targetFieldRenderer.material = targetFieldMaterial;
        }
        else
        {
            targetFieldRenderer.material = defaultMaterial;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.onDeath.AddListener(OnEnemyDeath);
            enemiesOnTarget.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesOnTarget.Remove(collision.gameObject);
        }
    }

    private void OnEnemyDeath(GameObject enemyGO)
    {
        if (enemiesOnTarget.Find(enemy => enemy == enemyGO))
        {
            enemiesOnTarget.Remove(enemyGO);
        }
    }
}
