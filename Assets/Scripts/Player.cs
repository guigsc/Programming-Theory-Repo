using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float horizontalMovement;
    private bool hasPressedJump;
    
    public bool IsGrounded;

    [SerializeField] private bool canAttack;
    private bool hasPressedAttack;

    private TargetField targetField;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded)
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
        playerRb.velocity = new Vector3(horizontalMovement * speed, playerRb.velocity.y, playerRb.velocity.z);

        if (hasPressedJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsGrounded = false;
            hasPressedJump = false;
        }

        if (hasPressedAttack)
        {
            hasPressedAttack = false;
            Attack();
        }
    }

    private void Attack()
    {
        foreach (var enemyGO in targetField.EnemiesOnTarget.ToList())
        {
            enemyGO.GetComponent<Enemy>().DealDamage(1);
        }
    }

    public void OnSensorTrigger(Sensor sensor)
    {
        this.canAttack = sensor.IsActive;
        this.targetField = sensor.TargetField;
    }
}
