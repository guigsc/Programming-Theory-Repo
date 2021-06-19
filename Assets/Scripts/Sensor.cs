using System;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    private UnityEvent<Sensor> onSensorTrigger;
    
    public bool IsActive { get; private set; }

    [SerializeField] TargetField m_targetField;
    public TargetField TargetField => m_targetField;

    private void Start()
    {
        SetTargetField();
        RegisterPlayers();
    }

    private void SetTargetField()
    {
        if (m_targetField != null)
        {
            onSensorTrigger = new UnityEvent<Sensor>();
            onSensorTrigger.AddListener(m_targetField.OnSensorTrigger);
        }
    }

    private void RegisterPlayers()
    {
        var players = FindObjectsOfType<Player>();
        foreach (var player in players)
        {
            onSensorTrigger.AddListener(player.OnSensorTrigger);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsActive = true;

            if (onSensorTrigger != null)
                onSensorTrigger.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsActive = false;

            if (onSensorTrigger != null)
                onSensorTrigger.Invoke(this);
        }
    }
}
