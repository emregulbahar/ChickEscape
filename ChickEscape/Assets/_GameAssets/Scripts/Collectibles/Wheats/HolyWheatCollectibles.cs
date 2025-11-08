using UnityEngine;
using UnityEngine.UI;
public class HolyWheatCollectibles : MonoBehaviour, ICollectible
{

    [SerializeField] private WheatDesingSO wheatDesingSO;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransfrom;

    private Image playerBoosterImage;

    private void Awake() {
        playerBoosterTransfrom = playerStateUI.GetBoosterJumpTransform;
        playerBoosterImage = playerBoosterTransfrom.GetComponent<Image>();
    }
    public void Collect()
    {
        playerController.SetJumpForce(wheatDesingSO.IncreaseDecreaseMultipler, wheatDesingSO.ResetBoostDuration);

        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransfrom, playerBoosterImage, playerStateUI.GetHolyBoosterWheatImage, wheatDesingSO.ActiveSprite, wheatDesingSO.PassiveSprite,
       wheatDesingSO.ActiveWheatSprite, wheatDesingSO.PassiveWheatSprite, wheatDesingSO.ResetBoostDuration);
        
        Destroy(gameObject);
    }
}
