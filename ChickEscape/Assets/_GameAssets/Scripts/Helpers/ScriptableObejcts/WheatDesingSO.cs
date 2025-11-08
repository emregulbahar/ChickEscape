using UnityEngine;


[CreateAssetMenu(fileName = "WhiteDesingSO", menuName = "ScriptableOjeckts/WheatDesingSO")]
public class WheatDesingSO : ScriptableObject
{
    [SerializeField] private float increaseDecreaseMultipler;

    [SerializeField] private float resetBoostDuration;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite passiveSprite;
    [SerializeField] private Sprite activeWheatSprite;
    [SerializeField] private Sprite passiveWheatSprite;



    public float IncreaseDecreaseMultipler => increaseDecreaseMultipler;

    public float ResetBoostDuration => resetBoostDuration;

    public Sprite ActiveSprite => activeSprite;
    
    public Sprite PassiveSprite => passiveSprite;

    public Sprite ActiveWheatSprite => activeWheatSprite;
    
    public Sprite PassiveWheatSprite => passiveWheatSprite;


}
    



    

