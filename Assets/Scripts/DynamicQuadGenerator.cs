using UnityEngine;

public class DynamicQuadGenerator : MonoBehaviour
{
    public bool isMove;
    public float safeRate;
    public float outRate;

    private bool isPressing = false;
    private float startX;
    private float endX;

    //セーフ判定のオブジェクト
    public GameObject safeobj;
    private SpriteRenderer safearea;
    private float safexmax;
    private float safexmin;


    //振動のスクリプト
    private OscillateSprite oscillateSprite;

    public KeikaiManager keikaiManager;


    void Start()
    {
        safearea = safeobj.GetComponent<SpriteRenderer>();
        oscillateSprite = GetComponent<OscillateSprite>();
    }

    void Update()
    {
        if (isMove)
        {
            HandleMouseInput();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック押下
        {
            isPressing = true;
            startX = transform.position.x; // 押し始めた時のオブジェクトのX座標を記録
            Debug.Log($"スタート{startX}");
        }

        if (Input.GetMouseButtonUp(0) && isPressing) // 左クリックを離した時
        {
            isPressing = false;
            endX = transform.position.x; // 離した時のオブジェクトのX座標を記録
            safexmax = safearea.bounds.max.x;
            safexmin = safearea.bounds.min.x;
            Debug.Log($"エンド{endX}");
            if (CheckSafe(startX,endX,safexmax,safexmin))//成功
            {
                safeRate =SafeCaluculate(startX,endX,safexmax,safexmin);
                Debug.Log($"成功{safeRate*100}％");

                //射精ゲージ

                //いたずらゲージ
            }
            else//失敗
            {
                outRate = OutCaluculate(startX, endX,oscillateSprite.rightBound, oscillateSprite.leftBound);
                Debug.Log($"失敗{outRate*100}％");

                //警戒度プラス
                keikaiManager.IncreaseAlert(outRate*100);
            }
        }
    }

    private bool CheckSafe(float startx, float endx, float maxsafex,float minsafex)
    {
        // maxとminx正しい順序に並べ替え
        float rangeMax = Mathf.Max(maxsafex, minsafex);
        float rangeMin = Mathf.Min(maxsafex, minsafex);

        float xMax = Mathf.Max(startx, endx);
        float xMin = Mathf.Min(startx, endx);

        //セーフ
        if (xMin >= rangeMin && xMax <= rangeMax)
        {
            return true;
        }
        //アウト
        else
        {
            return false;
        }
    }

    private float SafeCaluculate(float startx, float endx, float maxsafex, float minsafex)
    {
        // maxとminx正しい順序に並べ替え
        float rangeMax = Mathf.Max(maxsafex, minsafex);
        float rangeMin = Mathf.Min(maxsafex, minsafex);

        float xMax = Mathf.Max(startx, endx);
        float xMin = Mathf.Min(startx, endx);

        return (xMax - xMin) / (rangeMax - rangeMin);
    }
    private float OutCaluculate(float startx, float endx, float gageMax,float gageMin)
    {

        float xMax = Mathf.Max(startx, endx);
        float xMin = Mathf.Min(startx, endx);

        return (xMax - xMin) / (gageMax - gageMin);
    }




}
