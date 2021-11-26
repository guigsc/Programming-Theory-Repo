using Scripts.Enum;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] TargetFieldPosition _targetFieldPosition;
    [SerializeField] protected float _speed;
    [SerializeField] private float _attackRate;
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _health;

    private float _nextAttack;

    private bool _hasReachedPosition;

    private TargetField _targetField;
    private Vector3 _targetPosition;

    public UnityEvent<Enemy> onDeath = new UnityEvent<Enemy>();

    private void Start()
    {
        GetTargetPosition();
    }

    private void Update()
    {
        if (!_hasReachedPosition)
        {
            MoveTo(_targetPosition);
        }
        else
        {
            Attack();
        }
    }

    private void GetTargetPosition()
    {
        _targetField = FindObjectsOfType<TargetField>()
            .Where(t => t.transform.position.x < 0 && t.TargetFieldPosition == _targetFieldPosition)
            .FirstOrDefault(); 

        if (_targetField != null)
            _targetPosition = _targetField.transform.position;
        else
            _targetPosition = Vector3.zero;
    }

    private void MoveTo(Vector3 position)
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (transform.position.x >= position.x)
        {
            _speed = 0;
            _hasReachedPosition = true;
        }
    }

    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            Castle.Instance.DealDamage(_attackDamage);
            _nextAttack = Time.time + _attackRate;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (onDeath != null)
            onDeath.Invoke(this);

        Destroy(gameObject, 0.2f);
    }
}
