using UnityEngine;

public class GoldWheatCollectibles : MonoBehaviour, ICollectible
{

    [SerializeField] private WheatDesingSO wheatDesingSO;
    [SerializeField] private PlayerController playerController;

    


    public void Collect()
    {
        playerController.SetMovSpeed(wheatDesingSO.IncreaseDecreaseMultipler, wheatDesingSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
