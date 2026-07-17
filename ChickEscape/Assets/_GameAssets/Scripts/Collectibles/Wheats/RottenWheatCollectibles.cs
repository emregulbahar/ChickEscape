using UnityEngine;
using UnityEngine.UI;
public class RottenWheatCollectibles : MonoBehaviour, ICollectible
{

    [SerializeField] private WheatDesingSO wheatDesingSO;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransfrom;

    private Image playerBoosterImage;

    private void Awake() {
        playerBoosterTransfrom = playerStateUI.GetBoosterSlowTransform;
        playerBoosterImage = playerBoosterTransfrom.GetComponent<Image>();
    }
    public void Collect()
    {
        playerController.SetMovSpeed(wheatDesingSO.IncreaseDecreaseMultipler, wheatDesingSO.ResetBoostDuration);


        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransfrom, playerBoosterImage, playerStateUI.GetRottenBoosterWheatImage, wheatDesingSO.ActiveSprite, wheatDesingSO.PassiveSprite,
        wheatDesingSO.ActiveWheatSprite, wheatDesingSO.PassiveWheatSprite, wheatDesingSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        
        Destroy(gameObject);
    }
        
    
}
