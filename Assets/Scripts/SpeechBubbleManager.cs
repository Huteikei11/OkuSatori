using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpeechBubbleManager : MonoBehaviour
{
    public GameObject speechBubblePrefab; // �����o����Prefab
    public Transform topLeft; // �͈͂̍���
    public Transform bottomRight; // �͈͂̉E��

    public float displayInterval = 2.0f; // �����o���̕\���Ԋu�i�b�j
    public int maxBubbles = 3; // �����ɕ\���ł��鐁���o���̍ő吔
    private Queue<GameObject> activeBubbles = new Queue<GameObject>();
    private Coroutine displayCoroutine;
    private int orderInLayerCounter = 0; // �����o���̏��ԊǗ�
    [SerializeField] SpriteResolver resolver;
    public int bubbleType;
    public TurnManager turnManager;
    public bool isPinkuNow;//�s���N�̐����o�����o�Ă��邩(TurnManager�������)

    private void Start()
    {
        StartDisplaying();
    }

    public void SetSettings(float interval, int maxCount) //�ݒ��ς���
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

    public void StopDisplaying() //�~�߂�
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
    }

    public void HurikaeriBubble()//�s���N�����o��
    {
        //�����o����I��
        resolver.SetCategoryAndLabel("�s���N", "���Ƃ肳�܁H");


        GameObject bubble = speechBubblePrefab;
        ShowBubble(bubble,true);//�����o����\��
    }

    private IEnumerator DisplayBubbles() //���Ԋu�ŕ\������
    {
        
        while (true)
        {
            yield return new WaitForSeconds(displayInterval);

            //�����o����I��
            int rnd = 0;
            switch (bubbleType)//�����o���̃^�C�v�ŕ���
            {
                case 0://��
                    rnd = Random.Range(1, 1);//�^�C�v���Ƃ̐����o���̐����E����
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("��", "���Ƃ肳�܁H");
                            break;
                    }
                    break;

                case 1://�S�̐�
                    rnd = Random.Range(1, 1);//�^�C�v���Ƃ̐����o���̐����E����
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("�S�̐�", "����Ȃɂ������񂾂���");
                            break;
                    }
                    break;

                case 2://�����S�̐�
                    rnd = Random.Range(1, 1);//�^�C�v���Ƃ̐����o���̐����E����
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("�����S�̐�", "����Ȃ񂩂�������");
                            break;
                    }
                    break;

                case 3://�����ꐺ
                    rnd = Random.Range(1, 1);//�^�C�v���Ƃ̐����o���̐����E����
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("�����ꐺ", "���߂����������Ȃ���");
                            break;
                    }
                    break;

                case 4://�吺
                    rnd = Random.Range(1, 1);//�^�C�v���Ƃ̐����o���̐����E����
                    switch (rnd)
                    {
                        case 1:
                            resolver.SetCategoryAndLabel("�吺", "������񂠂���");
                            break;
                    }
                    break;
            }
            GameObject bubble = speechBubblePrefab;//��
            ShowBubble(bubble,false);//�����o����\������
        }
    }


    private void ShowBubble(GameObject Bubble,bool pinku) //�����o�����o������
    {
        if (!pinku || (pinku&&!isPinkuNow)) //�s���N�����o���ł͂Ȃ��A�܂���(�s���N�����o�����s���N�����o�����o�Ă��Ȃ�)
        {
            if (activeBubbles.Count >= maxBubbles)//�����o���ő吔�𒴂��Ă��Ȃ���
            {
                Destroy(activeBubbles.Dequeue());
            }

            Vector3 randomPosition = new Vector3(
                Random.Range(topLeft.position.x, bottomRight.position.x),
                Random.Range(bottomRight.position.y, topLeft.position.y),
                0f
            );

            GameObject newBubble = Instantiate(Bubble, randomPosition, Quaternion.identity);//�����o���𐶐�
            SpriteRenderer spriteRenderer = newBubble.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = ++orderInLayerCounter; // �V���������o���قǏ�ɕ\��
            }
            activeBubbles.Enqueue(newBubble);//�L���[�ɒǉ�
            StartCoroutine(DestroyBubbleAfterTime(newBubble, 15f));//�V���������o���Ɏ��Ԑ�����t����
            if (pinku)
            {
                isPinkuNow = true;//�s���N�����o���͕\����
                turnManager.Turn();//�U�����
            }
        }
    }

    private IEnumerator DestroyBubbleAfterTime(GameObject bubble, float delay)//�����o���������Ŕj��
    {
        yield return new WaitForSeconds(delay);
        if (activeBubbles.Contains(bubble))
        {
            activeBubbles.Dequeue();
        }
        Destroy(bubble);
    }
}
