using System.Collections;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public float holdTimeToDodge = 2f; // Inspector �Őݒ�\
    public bool isDodging = false;
    private float rightClickHoldTime = 0f;

    public SatoriController satoriController = null;
    public DynamicQuadGenerator dynamicQuadGenerator;
    public OscillateSprite oscillateSprite;

    void Update()
    {
        if (Input.GetMouseButton(1)) // �E�N���b�N��������Ă���
        {
            rightClickHoldTime += Time.deltaTime;
            if (!isDodging && rightClickHoldTime >= holdTimeToDodge)
            {
                StartDodge();
                rightClickHoldTime = 0f; // �J�E���g�����Z�b�g
            }
            else if (isDodging && rightClickHoldTime >= holdTimeToDodge)
            {
                StopDodge();
                rightClickHoldTime = 0f; // �J�E���g�����Z�b�g
            }
        }
        else
        {
            rightClickHoldTime = 0f;
        }
    }

    private void StartDodge()
    {
        isDodging = true;
        dynamicQuadGenerator.isMove = !isDodging; //��𒆂̓Z�N�n���ł��Ȃ�
        oscillateSprite.isMove = !isDodging;//��𒆂̓o�[���~�߂�
        satoriController.StartDodge();
        Debug.Log("�����Ԃɓ���܂���");
    }

    public void StopDodge()
    {
        isDodging = false;
        dynamicQuadGenerator.isMove = !isDodging;//�Z�N�n�����ĊJ
        oscillateSprite.isMove= !isDodging;//�o�[�ړ��ĊJ
        satoriController.StopDoge();
        Debug.Log("�����Ԃ��������܂���");
    }
}
