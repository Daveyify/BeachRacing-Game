using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer = 0;
    public TMP_Text textTimer;

    int timeMinutes, timeSeconds;

    void Update()
    {
        timer += Time.deltaTime;

        timeMinutes = Mathf.FloorToInt(timer/60);
        timeSeconds = Mathf.FloorToInt(timer % 60);

        textTimer.text = string.Format("{0:00}:{1:00}", timeMinutes, timeSeconds);
    }
   
}
