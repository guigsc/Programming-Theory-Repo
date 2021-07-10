using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody playerRb;
    private bool isGrounded;

    private float horizontalMovement;
    private bool hasPressedJump;

    private bool canAttack;
    private bool hasPressedAttack;
    private TargetField targetField;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            hasPressedJump = true;
        }

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            hasPressedAttack = true;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (hasPressedJump)
            Jump();

        if (hasPressedAttack)
            Attack();
    }

    private void Move()
    {
        playerRb.velocity = new Vector3(horizontalMovement * speed, playerRb.velocity.y, playerRb.velocity.z);
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        hasPressedJump = false;
    }

    private void Attack()
    {
        foreach (var enemy in targetField.EnemiesOnTarget.ToList())
        {
            enemy.DealDamage(1);
        }
        hasPressedAttack = false;
    }

    public void OnSensorTrigger(Sensor sensor)
    {
        canAttack = sensor.IsActive;
        targetField = sensor.TargetField;
    }

    public void OnGroundCollision()
    {
        isGrounded = true;
    }
}
