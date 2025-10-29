using UnityEngine;

public class HolyWheatCollectibles : MonoBehaviour, ICollectible
{

    [SerializeField] private WheatDesingSO wheatDesingSO;

    [SerializeField] private PlayerController playerController;


    public void Collect()
    {
        playerController.SetJumpForce(wheatDesingSO.IncreaseDecreaseMultipler, wheatDesingSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
