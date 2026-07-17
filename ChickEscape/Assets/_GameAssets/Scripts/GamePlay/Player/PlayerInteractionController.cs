using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    [SerializeField] private Transform _playerVisualTransform;

    private PlayerController playerController;
    private Rigidbody _playerRigidBody;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.TryGetComponent<IBoostables>(out var boostable))
        {
            boostable.Boost(playerController);
        }
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidBody, _playerVisualTransform);
            CameraShake.Instance.ShakeCamera(1f, 0.5f);
        }
    }
}
