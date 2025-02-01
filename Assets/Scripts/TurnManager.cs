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
        if (!ecchiState)//�G�b�`���=false�Ȃ� 
        {
            StartCoroutine(Hurimuki());
        }
    }

    public IEnumerator Hurimuki()
    {
        yield return new WaitForSeconds(waittime);//�����o�����o�Ă���A�j���[�V�����܂ł̎���
        Debug.Log("go");
        hideUI();//�o�[�Ǝ��Ԃ��B��
        animation();//����A�j���[�W�V����
        yield return new WaitForSeconds(1);//�A�j���[�V�����ɂ����鎞��

        if (dodgeController.isDodging)//������Ă��邩?
        {
        //�Z�[�t

            //�����߂�
            dodgeController.StopDodge();
            viewUI();//UI���ɖ߂�
            Debug.Log("��𐬌�");
        }
        else
        {
        //�A�E�g
            Debug.Log("������s");
            viewUI();//UI���ɖ߂�
        }
        //�s���N�����o�������Ԃ�Ȃ��悤�ɂ��鏈��_�n�܂�
        yield return new WaitForSeconds(4);//�A�j���[�V������ҋ@����
        speechBubbleManager.isPinkuNow = false;//�s���N�����o���͏����܂���
        //�s���N�����o�������Ԃ�Ȃ��悤�ɂ��鏈��_�I���
    }

    void animation()//�����������A�j���[�V����
    {

    }

    void hideUI()//UI�B��
    {
        dynamicQuadGenerator.isMove = false;
    }

    void viewUI()//UI���ɖ߂�
    {
        dynamicQuadGenerator.isMove = true;
    }
}
