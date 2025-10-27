using UnityEngine;

public class RottenWheatCollectibles : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float movIncreaseSpeed;

    [SerializeField] private float resetBoostDuration;


    public void Collect()
    {
        playerController.SetMovSpeed(movIncreaseSpeed, resetBoostDuration);
        Destroy(gameObject);
    }
        
    
}
