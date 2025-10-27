using UnityEngine;

public class GoldWheatCollectibles : MonoBehaviour
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
