using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    private UnityEvent _onGroundCollision = new UnityEvent();
    
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _onGroundCollision.AddListener(_player.OnGroundCollision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layer.Floor.ToString()))
        {
            if (_onGroundCollision != null)
                _onGroundCollision.Invoke();
        }
    }
}
