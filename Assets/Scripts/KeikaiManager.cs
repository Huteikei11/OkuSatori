using System.Collections;
using UnityEngine;

public class KeikaiManager : MonoBehaviour
{
    public float baseTurnInterval = 5f; // ステージごとの振り向き間隔（例: 5秒）
    public float interval;
    public float pinktimer;//閲覧用
    public float timetime;//閲覧用
    public float alertLevel = 0f; // 警戒度 (0 ~ 100)
    public float defaultAlertLevel = 20;
    private Coroutine turnRoutine;
    private Coroutine alertDecayRoutine;
    public float alertDecayRate = 1f; // 1秒ごとに減少する警戒度
    public SpeechBubbleManager speechBubbleManager;
    private float beforeTime;

 


    void Start()
    {
        alertLevel = defaultAlertLevel;
        interval = CaluculateInterval();
        StartAlertDecayRoutine();//警戒度下げるのを繰り返す
        beforeTime = Time.time; //今の時間
    }
    void Update()
    {
        timetime = Time.time;
        pinktimer = beforeTime + interval;
        if (Time.time >= pinktimer) //前回の時間+interval
        {
            beforeTime = Time.time;//今の時間を記録
            Turn();
        }
        
    }


    private float CaluculateInterval()//ピンクインターバルを計算
    {
        return (baseTurnInterval - 1) * (1 - (alertLevel / 100)) + 10;
    }

    private void StartAlertDecayRoutine()//警戒度下げる繰り返しを呼び出す
    {
        if (alertDecayRoutine != null)
        {
            StopCoroutine(alertDecayRoutine);
        }
        alertDecayRoutine = StartCoroutine(AlertDecayRoutine());
    }

    private IEnumerator AlertDecayRoutine() //警戒度を下げるのを繰り返す
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            DecreaseAlert(alertDecayRate);
        }
    }
    private void DecreaseAlert(float amount) //警戒度を下げるところ
    {
        alertLevel = Mathf.Clamp(alertLevel - amount, defaultAlertLevel, 100);
        //Debug.Log("Alert Level: " + alertLevel);
    }

    private void Turn()//一定間隔のピンク吹き出し
    {
        speechBubbleManager.HurikaeriBubble();//ピンク吹き出し
        Debug.Log("ピンク吹き出し一定間隔");
    }

    public void IncreaseAlert(float amount)
    {
        alertLevel = Mathf.Clamp(alertLevel + amount, 0, 100);
        //Debug.Log("Alert Level: " + alertLevel);
        if (alertLevel >= 100)
        {
            OnMaxAlert();
        }
        interval = CaluculateInterval(); // 警戒度が変わるたびに間隔を再計算
    }

    private void OnMaxAlert()
    {
        speechBubbleManager.HurikaeriBubble();//ピンク吹き出し
        alertLevel -= 1;
        Debug.Log("警戒度マックス！");
        // ここに100%時の処理を書く
    }
}
