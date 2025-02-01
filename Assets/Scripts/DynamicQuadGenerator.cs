using UnityEngine;

public class DynamicQuadGenerator : MonoBehaviour
{
    public bool isMove;
    public float safeRate;
    public float outRate;

    private bool isPressing = false;
    private float startX;
    private float endX;

    //�Z�[�t����̃I�u�W�F�N�g
    public GameObject safeobj;
    private SpriteRenderer safearea;
    private float safexmax;
    private float safexmin;


    //�U���̃X�N���v�g
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
        if (Input.GetMouseButtonDown(0)) // ���N���b�N����
        {
            isPressing = true;
            startX = transform.position.x; // �����n�߂����̃I�u�W�F�N�g��X���W���L�^
            Debug.Log($"�X�^�[�g{startX}");
        }

        if (Input.GetMouseButtonUp(0) && isPressing) // ���N���b�N�𗣂�����
        {
            isPressing = false;
            endX = transform.position.x; // ���������̃I�u�W�F�N�g��X���W���L�^
            safexmax = safearea.bounds.max.x;
            safexmin = safearea.bounds.min.x;
            Debug.Log($"�G���h{endX}");
            if (CheckSafe(startX,endX,safexmax,safexmin))//����
            {
                safeRate =SafeCaluculate(startX,endX,safexmax,safexmin);
                Debug.Log($"����{safeRate*100}��");

                //�ː��Q�[�W

                //��������Q�[�W
            }
            else//���s
            {
                outRate = OutCaluculate(startX, endX,oscillateSprite.rightBound, oscillateSprite.leftBound);
                Debug.Log($"���s{outRate*100}��");

                //�x���x�v���X
                keikaiManager.IncreaseAlert(outRate*100);
            }
        }
    }

    private bool CheckSafe(float startx, float endx, float maxsafex,float minsafex)
    {
        // max��minx�����������ɕ��בւ�
        float rangeMax = Mathf.Max(maxsafex, minsafex);
        float rangeMin = Mathf.Min(maxsafex, minsafex);

        float xMax = Mathf.Max(startx, endx);
        float xMin = Mathf.Min(startx, endx);

        //�Z�[�t
        if (xMin >= rangeMin && xMax <= rangeMax)
        {
            return true;
        }
        //�A�E�g
        else
        {
            return false;
        }
    }

    private float SafeCaluculate(float startx, float endx, float maxsafex, float minsafex)
    {
        // max��minx�����������ɕ��בւ�
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
