using UnityEngine;


[CreateAssetMenu(fileName = "WhiteDesingSO", menuName = "ScriptableOjeckts/WheatDesingSO")]
public class WheatDesingSO : ScriptableObject
{
    [SerializeField] private float increaseDecreaseMultipler;

    [SerializeField] private float resetBoostDuration;




    public float IncreaseDecreaseMultipler => increaseDecreaseMultipler;

    public float ResetBoostDuration => resetBoostDuration;

}
    



    

