using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpeechBubbleManager : MonoBehaviour
{
    public GameObject speechBubblePrefab; // 吹き出しのPrefab
    public Transform topLeft; // 範囲の左上
    public Transform bottomRight; // 範囲の右下

    public float displayInterval = 2.0f; // 吹き出しの表示間隔（秒）
    public int maxBubbles = 3; // 同時に表示できる吹き出しの最大数
    private Queue<GameObject> activeBubbles = new Queue<GameObject>();
    private Coroutine displayCoroutine;
    private int orderInLayerCounter = 0; // 吹き出しの順番管理
    [SerializeField] SpriteResolver resolver;
    public int bubbleType;
    public TurnManager turnManager;
    public bool isPinkuNow;//ピンクの吹き出しが出ているか(TurnManagerから解除)

    private void Start()
    {
        StartDisplaying();
    }

    public void SetSettings(float interval, int maxCount) //設定を変える
    {
        displayInterval = interval;
        maxBubbles = maxCount;
    }

    public void StartDisplaying()
    {
        if (displayCoroutine == null)
        {
            displayCoroutine = StartCoroutine(DisplayBubbles());
        }
    }

    public void StopDisplaying() //止める
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
    }

    public void HurikaeriBubble()//ピンク吹き出し
    {
        //吹き出しを選ぶ
        resolver.SetCategoryAndLabel("ピンク", "さとりさま？");


        GameObject bubble = speechBubblePrefab;
        ShowBubble(bubble,true);//吹き出しを表示
    }

    private IEnumerator DisplayBubbles() //一定間隔で表示命令
    {
        
        while (true)
        {
            yield return new WaitForSeconds(displayInterval);

            //吹き出しを選ぶ
            int rnd = 0;
            switch (bubbleType)//吹き出しのタイプで分岐
            {
                case 0://声
                    rnd = Random.Range(1, 1);//タイプごとの吹き出しの数を右側に
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("声", "さとりさま？");
                            break;
                    }
                    break;

                case 1://心の声
                    rnd = Random.Range(1, 1);//タイプごとの吹き出しの数を右側に
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("心の声", "あれなにさがすんだっけ");
                            break;
                    }
                    break;

                case 2://混乱心の声
                    rnd = Random.Range(1, 1);//タイプごとの吹き出しの数を右側に
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("混乱心の声", "あれなんかおかしい");
                            break;
                    }
                    break;

                case 3://かすれ声
                    rnd = Random.Range(1, 1);//タイプごとの吹き出しの数を右側に
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("かすれ声", "だめこえおさえなきゃ");
                            break;
                    }
                    break;

                case 4://大声
                    rnd = Random.Range(1, 1);//タイプごとの吹き出しの数を右側に
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("大声", "ああんんあああ");
                            break;
                    }
                    break;
            }
            GameObject bubble = speechBubblePrefab;//仮
            ShowBubble(bubble,false);//吹き出しを表示する
        }
    }


    private void ShowBubble(GameObject Bubble,bool pinku) //吹き出しを出す部分
    {
        if (!pinku || (pinku&&!isPinkuNow)) //ピンク吹き出しではない、または(ピンク吹き出しかつピンク吹き出しが出ていない)
        {
            if (activeBubbles.Count >= maxBubbles)//吹き出し最大数を超えていないか
            {
                Destroy(activeBubbles.Dequeue());
            }

            Vector3 randomPosition = new Vector3(
                Random.Range(topLeft.position.x, bottomRight.position.x),
                Random.Range(bottomRight.position.y, topLeft.position.y),
                0f
            );

            GameObject newBubble = Instantiate(Bubble, randomPosition, Quaternion.identity);//吹き出しを生成
            SpriteRenderer spriteRenderer = newBubble.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = ++orderInLayerCounter; // 新しい吹き出しほど上に表示
            }
            activeBubbles.Enqueue(newBubble);//キューに追加
            StartCoroutine(DestroyBubbleAfterTime(newBubble, 15f));//新しい吹き出しに時間制限を付ける
            if (pinku)
            {
                isPinkuNow = true;//ピンク吹き出しは表示中
                turnManager.Turn();//振り向く
            }
        }
    }

    private IEnumerator DestroyBubbleAfterTime(GameObject bubble, float delay)//吹き出しを自動で破壊
    {
        yield return new WaitForSeconds(delay);
        if (activeBubbles.Contains(bubble))
        {
            activeBubbles.Dequeue();
        }
        Destroy(bubble);
    }
}
