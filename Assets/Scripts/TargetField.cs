using Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetField : MonoBehaviour
{
    MeshRenderer targetFieldRenderer;

    [SerializeField] Material targetFieldMaterial;
    [SerializeField] Material defaultMaterial;

    [SerializeField] private TargetFieldPosition targetFieldPosition;

    private List<Enemy> enemiesOnTarget;
    public List<Enemy> EnemiesOnTarget => enemiesOnTarget; 

    public TargetFieldPosition TargetFieldPosition => targetFieldPosition;

    private void Start()
    {
        targetFieldRenderer = GetComponent<MeshRenderer>();
        enemiesOnTarget = new List<Enemy>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.onDeath.AddListener(OnEnemyDeath);
            enemiesOnTarget.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesOnTarget.Remove(other.GetComponent<Enemy>());
        }
    }

    private void OnEnemyDeath(Enemy deadEnemy)
    {
        if (enemiesOnTarget.Find(enemy => enemy == deadEnemy))
        {
            enemiesOnTarget.Remove(deadEnemy);
        }
    }
}
