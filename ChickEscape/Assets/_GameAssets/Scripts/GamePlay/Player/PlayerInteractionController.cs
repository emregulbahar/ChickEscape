using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
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
}
