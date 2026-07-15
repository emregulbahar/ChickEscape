using UnityEngine;
using TMPro;
using DG.Tweening;
public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform timerRotatableTransform;
    [SerializeField] private TMP_Text timerText;

    [Header("Settings")]
    [SerializeField] private float rotationDuration;
    [SerializeField] private Ease rotationEase;



    private float elapsedTime;
    private bool _isTimerRunning;
    private Tween _rotationTween;
    private string _finalTime;


    private void Start() 
    {
        PlayRotationAnimation();
        StartTimer();

        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }


    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Pause:
                StopTimer();
                break;

            case GameState.Resume:
                ResumeTimer();
                break;
            
            case GameState.GameOver:
                FinishTimer();
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        _rotationTween = timerRotatableTransform.DORotate(new Vector3(0f, 0f, -360f), rotationDuration, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(rotationEase);
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
        elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
        CancelInvoke(nameof(UpdateTimerUI));
        _rotationTween.Pause();
    }

      private void ResumeTimer()
    {
        if(!_isTimerRunning){
            _isTimerRunning = true;
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            _rotationTween.Play();
        }
    }

    private void FinishTimer()
    {
        StopTimer();
        _finalTime = GetFormattedElapsedTime();
    }

    private string GetFormattedElapsedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int second = Mathf.FloorToInt(elapsedTime % 60f);

        return string.Format("{0:00}:{1:00}", minutes, second);
    }


    private void UpdateTimerUI()
    {

        if (!_isTimerRunning)
        {
            return;
        }

        elapsedTime += 1;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int second = Mathf.FloorToInt(elapsedTime % 60f);


        timerText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }


    public string GetFinalTime()
    {
        return _finalTime;
    }

}
