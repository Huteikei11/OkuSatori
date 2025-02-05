using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DynamicQuadGenerator : MonoBehaviour
{
    public bool Mode;
    public bool isArea;
    public bool isMove;
    [Header("�������s���̉��Z�l")]
    public float itazura;
    public float syasei;
    public float keikai;
    [Header("�v�ɂȂ���p�̕ϐ�")]
    public float safeRate;
    public float outRate;

    private bool isPressing = false;
    private float startX;
    private float endX;
    [Header("�ǂݍ��ނ���")]
    //�Z�[�t����̃I�u�W�F�N�g
    public GameObject safeobj;
    private SpriteRenderer safearea;
    private float safexmax;
    private float safexmin;
    public goodbad goodbad;


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
        if (!Mode)//�ŏ��ɍ�������̓��_�v�Z
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
                if (CheckSafe(startX, endX, safexmax, safexmin))//����
                {
                    safeRate = SafeCaluculate(startX, endX, safexmax, safexmin);
                    Debug.Log($"����{safeRate * 100}��");

                    //�ː��Q�[�W

                    //��������Q�[�W
                }
                else//���s
                {
                    outRate = OutCaluculate(startX, endX, oscillateSprite.rightBound, oscillateSprite.leftBound);
                    Debug.Log($"���s{outRate * 100}��");

                    //�x���x�v���X
                    keikaiManager.IncreaseAlert(outRate * 100);
                }
            }
        }
        else//�����Ă���Ԍv�Z�����
        {
            if (Input.GetMouseButtonDown(0)) // ���N���b�N����
            {
                isPressing = true;
            }
            if(Input.GetMouseButtonUp(0) && isPressing) // ���N���b�N�𗣂�����
            {
                isPressing = false;
                
            }
            if (isPressing)
            {
                if (isArea)//����
                {
                    Debug.Log($"����");
                    goodbad.PopJuge(1);
                }
                else//���s
                {
                    Debug.Log($"���s");
                    goodbad.PopJuge(2);

                    //�x���x�v���X
                    keikaiManager.IncreaseAlert(keikai);
                }
            }
        }
    }



    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log($"{collision.gameObject.tag}");
        if(collision.gameObject.tag == "SafeArea")
        {
            isArea = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SafeArea")
        {
            isArea = false;
            goodbad.PopJuge(0);//����̃}�[�N������

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
