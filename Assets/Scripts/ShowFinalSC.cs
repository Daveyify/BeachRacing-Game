using TMPro;
using UnityEngine;

public class ShowFinalSC : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public int balls;

    void Start()
    {
        balls = PlayerPrefs.GetInt("balls");
        UpdateText();
    }

    public void UpdateText()
    {
        finalScoreText.text = "Total balls: " + balls.ToString();
    }
}
