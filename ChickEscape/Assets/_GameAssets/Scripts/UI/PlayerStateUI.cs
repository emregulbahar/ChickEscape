using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RectTransform playerWalkingTransform;
    [SerializeField] private RectTransform playerSlidingTransform;
    [SerializeField] private RectTransform boosterSpeedTransform;
    [SerializeField] private RectTransform boosterJumpTransform;
    [SerializeField] private RectTransform boosterSlowTransform;

    [Header("Images")]
    [SerializeField] private Image goldBoosterWheatImage;
    [SerializeField] private Image holyBoosterWheatImage;
    [SerializeField] private Image rottenBoosterWheatImage;


    [Header("Sprites")]

    [SerializeField] private Sprite playerWalkingActiveSprite;

    [SerializeField] private Sprite playerWalkingPassiveSprite;

    [SerializeField] private Sprite playerSlidingActiveSprite;

    [SerializeField] private Sprite playerSlidingPassiveSprite;

    [Header("Settings")]

    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase;

    public RectTransform GetBoosterSpeedTransform => boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => boosterSlowTransform;

    public Image GetGoldBoosterWheatImage => goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => rottenBoosterWheatImage;  
    private Image playerWalkingImage;
    private Image pLayerSlidingImage;



    private void Awake() {
        playerWalkingImage = playerWalkingTransform.GetComponent<Image>();
        pLayerSlidingImage = playerSlidingTransform.GetComponent<Image>();
        }

    private void Start()
    {
        playerController.OnPlayerStateChange += PlayerController_OnPlayerStateChange;

        SetStateUserInterfaces(playerWalkingActiveSprite, playerSlidingPassiveSprite, playerWalkingTransform, playerSlidingTransform);

    }


    private void PlayerController_OnPlayerStateChange(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(playerWalkingActiveSprite, playerSlidingPassiveSprite, playerWalkingTransform, playerSlidingTransform);
                break;
            case PlayerState.SlideIdle:
            case PlayerState.Slide:
            SetStateUserInterfaces(playerWalkingPassiveSprite, playerSlidingActiveSprite, playerSlidingTransform, playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite, RectTransform activeTransform,
    RectTransform passiveTransform)
    {
        playerWalkingImage.sprite = playerWalkingSprite;
        pLayerSlidingImage.sprite = playerSlidingSprite;


        activeTransform.DOAnchorPosX(-25f, moveDuration).SetEase(moveEase);
        passiveTransform.DOAnchorPosX(-90f, moveDuration).SetEase(moveEase);
    }



    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite,
    Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(-25F, moveDuration).SetEase(moveEase);


        yield return new WaitForSeconds(duration);


        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90F, moveDuration).SetEase(moveEase);
    }


    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite,
    Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }


}
