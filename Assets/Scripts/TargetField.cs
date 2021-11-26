using Assets.Scripts.Enums;
using Scripts.Enum;
using System.Collections.Generic;
using UnityEngine;

public class TargetField : MonoBehaviour
{
    [SerializeField] private Material _targetFieldMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private TargetFieldPosition _targetFieldPosition;
    
    private MeshRenderer _targetFieldRenderer;
    
    private List<Enemy> _enemiesOnTarget;

    public List<Enemy> EnemiesOnTarget => _enemiesOnTarget;
    public TargetFieldPosition TargetFieldPosition => _targetFieldPosition;

    private void Awake()
    {
        _targetFieldRenderer = GetComponent<MeshRenderer>();
        _enemiesOnTarget = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Enemy.ToString()))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.onDeath.AddListener(OnEnemyDeath);
            _enemiesOnTarget.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Enemy.ToString()))
        {
            _enemiesOnTarget.Remove(other.GetComponent<Enemy>());
        }
    }

    private void OnEnemyDeath(Enemy deadEnemy)
    {
        if (_enemiesOnTarget.Find(enemy => enemy == deadEnemy))
        {
            _enemiesOnTarget.Remove(deadEnemy);
        }
    }

    public void OnSensorTrigger(Sensor sensor)
    {
        if (sensor.IsActive)
        {
            _targetFieldRenderer.material = _targetFieldMaterial;
        }
        else
        {
            _targetFieldRenderer.material = _defaultMaterial;
        }
    }
}
