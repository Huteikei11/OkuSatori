using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool ecchiState;
    public float waittime;
    public DodgeController dodgeController;
    public DynamicQuadGenerator dynamicQuadGenerator;
    public SpeechBubbleManager speechBubbleManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Turn() 
    {
        if (!ecchiState)//エッチ状態=falseなら 
        {
            StartCoroutine(Hurimuki());
        }
    }

    public IEnumerator Hurimuki()
    {
        yield return new WaitForSeconds(waittime);//吹き出しが出てからアニメーションまでの時間
        Debug.Log("go");
        hideUI();//バーと時間を隠す
        animation();//お空アニメージション
        yield return new WaitForSeconds(1);//アニメーションにかかる時間

        if (dodgeController.isDodging)//回避しているか?
        {
        //セーフ

            //回避を戻す
            dodgeController.StopDodge();
            viewUI();//UI元に戻す
            Debug.Log("回避成功");
        }
        else
        {
        //アウト
            Debug.Log("回避失敗");
            viewUI();//UI元に戻す
        }
        //ピンク吹き出しがかぶらないようにする処理_始まり
        yield return new WaitForSeconds(4);//アニメーション後待機時間
        speechBubbleManager.isPinkuNow = false;//ピンク吹き出しは消えました
        //ピンク吹き出しがかぶらないようにする処理_終わり
    }

    void animation()//おくうちゃんアニメーション
    {

    }

    void hideUI()//UI隠す
    {
        dynamicQuadGenerator.isMove = false;
    }

    void viewUI()//UI元に戻す
    {
        dynamicQuadGenerator.isMove = true;
    }
}
