using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] TargetField _targetField;

    private UnityEvent<Sensor> _onSensorTrigger;
    private bool _isActive;

    public bool IsActive => _isActive;
    public TargetField TargetField => _targetField;

    private void Awake()
    {
        _onSensorTrigger = new UnityEvent<Sensor>();

        SetTargetField();
        RegisterPlayers();
    }

    private void SetTargetField()
    {
        if (_targetField != null)
        {
            _onSensorTrigger.AddListener(_targetField.OnSensorTrigger);
        }
    }

    private void RegisterPlayers()
    {
        foreach (var player in FindObjectsOfType<Player>())
        {
            _onSensorTrigger.AddListener(player.OnSensorTrigger);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player.ToString()))
        {
            _isActive = true;

            if (_onSensorTrigger != null)
                _onSensorTrigger.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player.ToString()))
        {
            _isActive = false;

            if (_onSensorTrigger != null)
                _onSensorTrigger.Invoke(this);
        }
    }
}
