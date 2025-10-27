using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState currentPlayerState = PlayerState.Idle;


    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }


    public void ChangeState(PlayerState newPlyaerState)
    {
        if (currentPlayerState == newPlyaerState)
        {
            return;
        }

        currentPlayerState = newPlyaerState;
    }


    public PlayerState GetCurrentState()
    {
        return currentPlayerState;
    }
}
