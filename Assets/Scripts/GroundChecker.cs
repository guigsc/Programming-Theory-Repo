using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    private UnityEvent onGroundCollision = new UnityEvent();
    
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        onGroundCollision.AddListener(player.OnGroundCollision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (onGroundCollision != null)
                onGroundCollision.Invoke();
        }
    }
}
