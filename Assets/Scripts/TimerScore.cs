using System.Collections;
using System.Collections.Generic;
using Dan.Demo;
using TMPro;
using UnityEngine;

public class TimerScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    float timer;
    public static int timerInt;
    void Start()
    {
        timer = 0;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        timerInt = (int)timer;
        text.text = timerInt.ToString();
        LeaderboardShowcase._playerScore = timerInt;
    }

    private void OnLeaderboardLoaded()
    {
        LeaderboardShowcase._playerScore = TimerScore.timerInt;
        LeaderboardShowcase.actualplayerScoreText.text = "Score: " + LeaderboardShowcase._playerScore;
    }
}
