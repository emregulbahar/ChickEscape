using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealtUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] playerHealthImage;

    [Header("Sprites")]
    [SerializeField] private Sprite playerHealtySprite;
    [SerializeField] private Sprite playerUnhealtySprite;

    [Header("Settings")]
    [SerializeField] private float scaleDuration;

    private RectTransform[] playerHealthTransform;


    private void Awake()
    {
        playerHealthTransform = new RectTransform[playerHealthImage.Length];

        for (int i = 0; i < playerHealthImage.Length; i++)
        {
            playerHealthTransform[i] = playerHealthImage[i].gameObject.GetComponent<RectTransform>();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AnimateDamage();
        }

         if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < playerHealthImage.Length; i++)
        {
            if (playerHealthImage[i].sprite == playerHealtySprite)
            {
                AnimateDamageSprite(playerHealthImage[i], playerHealthTransform[i]);
                break;
            }
        }
    }
    
    public void AnimateDamageForAll()
    {
        for (int i = 0; i < playerHealthImage.Length; i++)
        {
            AnimateDamageSprite(playerHealthImage[i], playerHealthTransform[i]);
        }
    }
    
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = playerUnhealtySprite;
            activeImageTransform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
