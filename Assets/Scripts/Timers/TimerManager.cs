using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float timeForWait;

    public List<GameObject> fans;

    private bool inProgress;
    private DateTime _timerStart;
    public DateTime timerStart { get { return _timerStart; } set { _timerStart = value; } }
    private DateTime currentTimerEnd;
    private DateTime _timerEnd;
    public DateTime timerEnd { get { return _timerEnd; } set { _timerEnd = value; } }

    [Header("UI/UX")]
    [SerializeField] TMP_Text timerText;

    [Header("Production Time")]
    public int minutes;
    public int seconds;

    Coroutine timerCoroutine;
    bool inprogress = false;

    private void OnEnable()
    {
        Assets.Scripts.NPC.OnCharmPerson += CharmPerson;
    }

    private void OnDisable()
    {
        Assets.Scripts.NPC.OnCharmPerson -= CharmPerson;
    }

    void Start()
    {
        if (timerCoroutine == null) timerCoroutine = StartCoroutine(StartTimerCoroutine());
    }

    void CharmPerson(GameObject obj)
    {
        if (!fans.Contains(obj))
        {
            fans.Add(obj);

            setTimer();

            inprogress = true;

            //StopAllCoroutines();
            //if (timerCoroutine == null) timerCoroutine = StartCoroutine(StartTimerCoroutine());
            Debug.Log("obj");
        }
    }

    void setTimer()
    {
        _timerStart = DateTime.Now;
        TimeSpan span = new TimeSpan(0, 0, minutes, seconds);
        _timerEnd = _timerStart.Add(span);
    }

    IEnumerator StartTimerCoroutine()
    {
        while (true)
        {
            DateTime start = DateTime.Now;
            currentTimerEnd = timerEnd;
            TimeSpan timeLeft = timerEnd - start;
            double totalSecondsLeft = timeLeft.TotalSeconds;
            string text;
            if (timerEnd != DateTime.MinValue)
            {
                if (timerEnd != null)
                {
                    if (currentTimerEnd != timerEnd)
                    {
                        start = DateTime.Now;
                        currentTimerEnd = timerEnd;
                        timeLeft = timerEnd - start;
                        totalSecondsLeft = timeLeft.TotalSeconds;
                    }

                    text = "";
                    if (totalSecondsLeft > 0)
                    {
                        if (timeLeft.Days != 0)
                        {
                            text += timeLeft.Days + "d ";
                            text += timeLeft.Hours + "h";

                            timerText.text = text;

                            yield return new WaitForSeconds(timeLeft.Minutes * 60);
                        }
                        else if (timeLeft.Hours != 0)
                        {
                            text += timeLeft.Hours + "h ";
                            text += timeLeft.Minutes + "m";

                            timerText.text = text;

                            yield return new WaitForSeconds(timeLeft.Seconds);
                        }
                        else if (timeLeft.Minutes != 0)
                        {
                            TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                            text += ts.Minutes + "m ";
                            text += ts.Seconds + "s";
                        }
                        else
                        {
                            text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                        }
                        timerText.text = text;

                        totalSecondsLeft -= Time.deltaTime;
                        yield return null;
                    }
                    else if(inprogress)
                    {
                        timerText.text = "Finished";
                        EndTimer();
                        //break;
                    }
                }

                
            }

            yield return null;
        }
    }
    
    void EndTimer()
    {
        timerText.text = "Timeros ended.";

        inprogress = false;

        setTimer();
        
        int fanIndex = (int)UnityEngine.Random.Range(0f, fans.Count);
        Assets.Scripts.NPC npc = fans[fanIndex].GetComponent<Assets.Scripts.NPC>();
        if (npc != null)
        {
            npc.SetHaterBehaviour();
        }
    }
}
