using System.Collections;
using UnityEngine;

public class KeikaiManager : MonoBehaviour
{
    public float baseTurnInterval = 5f; // �X�e�[�W���Ƃ̐U������Ԋu�i��: 5�b�j
    public float interval;
    public float pinktimer;//�{���p
    public float timetime;//�{���p
    public float alertLevel = 0f; // �x���x (0 ~ 100)
    public float defaultAlertLevel = 20;
    private Coroutine turnRoutine;
    private Coroutine alertDecayRoutine;
    public float alertDecayRate = 1f; // 1�b���ƂɌ�������x���x
    public SpeechBubbleManager speechBubbleManager;
    private float beforeTime;

 


    void Start()
    {
        alertLevel = defaultAlertLevel;
        interval = CaluculateInterval();
        StartAlertDecayRoutine();//�x���x������̂��J��Ԃ�
        beforeTime = Time.time; //���̎���
    }
    void Update()
    {
        timetime = Time.time;
        pinktimer = beforeTime + interval;
        if (Time.time >= pinktimer) //�O��̎���+interval
        {
            beforeTime = Time.time;//���̎��Ԃ��L�^
            Turn();
        }
        
    }


    private float CaluculateInterval()//�s���N�C���^�[�o�����v�Z
    {
        return (baseTurnInterval - 1) * (1 - (alertLevel / 100)) + 10;
    }

    private void StartAlertDecayRoutine()//�x���x������J��Ԃ����Ăяo��
    {
        if (alertDecayRoutine != null)
        {
            StopCoroutine(alertDecayRoutine);
        }
        alertDecayRoutine = StartCoroutine(AlertDecayRoutine());
    }

    private IEnumerator AlertDecayRoutine() //�x���x��������̂��J��Ԃ�
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            DecreaseAlert(alertDecayRate);
        }
    }
    private void DecreaseAlert(float amount) //�x���x��������Ƃ���
    {
        alertLevel = Mathf.Clamp(alertLevel - amount, defaultAlertLevel, 100);
        //Debug.Log("Alert Level: " + alertLevel);
    }

    private void Turn()//���Ԋu�̃s���N�����o��
    {
        speechBubbleManager.HurikaeriBubble();//�s���N�����o��
        Debug.Log("�s���N�����o�����Ԋu");
    }

    public void IncreaseAlert(float amount)
    {
        alertLevel = Mathf.Clamp(alertLevel + amount, 0, 100);
        //Debug.Log("Alert Level: " + alertLevel);
        if (alertLevel >= 100)
        {
            OnMaxAlert();
        }
        interval = CaluculateInterval(); // �x���x���ς�邽�тɊԊu���Čv�Z
    }

    private void OnMaxAlert()
    {
        speechBubbleManager.HurikaeriBubble();//�s���N�����o��
        alertLevel -= 1;
        Debug.Log("�x���x�}�b�N�X�I");
        // ������100%���̏���������
    }
}
