using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private int _health;

    private static Castle _instance;
    public static Castle Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void DealDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            _health = 0;
            // Game Over
        }
    }
}
