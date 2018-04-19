using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    [SerializeField] TextMesh text;
    [SerializeField] int maxTime;
    DateTime startTime;
    DateTime endTime;
    DateTime currentTime;

	void Start () {
        startTime = DateTime.Now;
        endTime = startTime + new TimeSpan(0, 0, maxTime);
	}

    void UpdateTime()
    {
        currentTime = DateTime.Now;
        TimeSpan remaining = endTime - currentTime;
        if (currentTime > endTime)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        string secondsString = (remaining.Seconds < 10) ? "0" + remaining.Seconds.ToString() : remaining.Seconds.ToString();
        int centiSeconds = (remaining.Milliseconds) / 10;
        string centiSecondsString = (centiSeconds < 10) ? "0" + centiSeconds.ToString() : centiSeconds.ToString();
        text.text = String.Format("{0}:{1}", secondsString, centiSecondsString);
    }
	
	void Update () {
        UpdateTime();
	}
}
