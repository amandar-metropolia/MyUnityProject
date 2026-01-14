using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    float _elapsedTime = 0f;

    void Update()
    {
        if (Time.timeScale > 0f)
            _elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(_elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((_elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
