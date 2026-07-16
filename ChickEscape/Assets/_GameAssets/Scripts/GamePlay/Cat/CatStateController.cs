using UnityEngine;

public class CatStateController : MonoBehaviour
{
    
    [SerializeField] private CatState _currentCatState = CatState.Walking;


     private void Start()
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newSate)
    {
        if(_currentCatState == newSate){return;}

        _currentCatState = newSate;
    }


    public CatState GetCurrentState()
    {
        return _currentCatState;
    }
}
