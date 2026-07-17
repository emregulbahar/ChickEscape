using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
   [SerializeField] private Animator _cataAnimator;

   private CatStateController _catStateController;


   private void Awake() 
   {
    
    _catStateController = GetComponent<CatStateController>();
   }

    private void Update() 
    {
         if(GameManager.Instance.GetCurrentGameState() != GameState.Play && 
        GameManager.Instance.GetCurrentGameState() != GameState.Resume && 
        GameManager.Instance.GetCurrentGameState() != GameState.CutScene )
        {
            
            _cataAnimator.enabled = false;
            return;
        }
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        _cataAnimator.enabled = true;
        var currentCatState = _catStateController.GetCurrentState();

        switch (currentCatState)
        {
            case CatState.Idle:
                _cataAnimator.SetBool(Consts.CatAnimations.IS_IDLING, true);
                _cataAnimator.SetBool(Consts.CatAnimations.IS_WALKING, false);
                _cataAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;    

            case CatState.Walking:
                _cataAnimator.SetBool(Consts.CatAnimations.IS_IDLING, false);
                _cataAnimator.SetBool(Consts.CatAnimations.IS_WALKING, true);
                _cataAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;

            case CatState.Runing:
                _cataAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, true);
                break;

            case CatState.Attacking:
                _cataAnimator.SetBool(Consts.CatAnimations.IS_ATTACKING, true);
                break;                  
        }


    }
    
}
