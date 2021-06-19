using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instance;

    [SerializeField] private int health;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            health = 0;
            // Game Over
        }



    }
}
