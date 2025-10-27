using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
           other.gameObject?.GetComponent<GoldWheatCollectibles>().Collect();
        }

        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            other.gameObject?.GetComponent<HolyWheatCollectibles>().Collect();
        }

         if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
           other.gameObject?.GetComponent<RottenWheatCollectibles>().Collect();
        }
    }
}
