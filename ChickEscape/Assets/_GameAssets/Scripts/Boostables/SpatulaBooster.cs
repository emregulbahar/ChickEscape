using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostables
{
    [Header("References")]
    [SerializeField] private Animator spatulaAnimator;

    [Header("Settings")]
    [SerializeField] private float jumpForce;



    private bool isActivated;

    public void Boost(PlayerController playerController)
    {

        if (isActivated) { return; }

        PlayBoostAnimations();
        Rigidbody rb = playerController.GetPlayerRigidbody();

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isActivated = true;

        Invoke(nameof(ResetActivation), 0.2f);
    }


    private void PlayBoostAnimations()
    {
        spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
    }

    private void ResetActivation()
    {
        isActivated = false;
    }
}
