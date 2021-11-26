using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _attackDamage;

    private Rigidbody _playerRb;
    private bool _isGrounded;

    private float _horizontalMovement;
    private bool _hasPressedJump;

    private bool _canAttack;
    private bool _hasPressedAttack;
    private TargetField _targetField;

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _hasPressedJump = true;
        }

        if (Input.GetButtonDown("Fire1") && _canAttack)
        {
            _hasPressedAttack = true;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (_hasPressedJump)
            Jump();

        if (_hasPressedAttack)
            Attack();
    }

    private void Move()
    {
        _playerRb.velocity = new Vector3(_horizontalMovement * _speed, _playerRb.velocity.y, _playerRb.velocity.z);
    }

    private void Jump()
    {
        _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false;
        _hasPressedJump = false;
    }

    private void Attack()
    {
        foreach (var enemy in _targetField.EnemiesOnTarget.ToList())
        {
            enemy.TakeDamage(_attackDamage);
        }
        _hasPressedAttack = false;
    }

    public void OnSensorTrigger(Sensor sensor)
    {
        _canAttack = sensor.IsActive;
        _targetField = sensor.TargetField;
    }

    public void OnGroundCollision()
    {
        _isGrounded = true;
    }
}
