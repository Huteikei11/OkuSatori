using UnityEngine;

public class OscillateSprite : MonoBehaviour
{
    [Header("�U���ݒ�")]
    [Tooltip("�U���̉E�[�̍��W")]
    public float rightBound = 3.0f;

    [Tooltip("�U���̍��[�̍��W")]
    public float leftBound = -3.0f;

    [Tooltip("�U���̎��� (�b)")]
    public float period = 2.0f;

    [Tooltip("�J�n���̈ʒu (0 ~ 1)")]
    [Range(0f, 1f)]
    public float startOffset = 0.0f;

    private float _amplitude; // �U��
    private float _centerX;   // ���S�ʒu

    public bool isMove;
    private float caltime;

    void Start()
    {
        // �U���ƒ��S�ʒu���v�Z
        _amplitude = (rightBound - leftBound) / 2.0f;
        _centerX = (rightBound + leftBound) / 2.0f;

        // �J�n�ʒu��ݒ�
        float initialX = _centerX + _amplitude * Mathf.Sin(startOffset * 2 * Mathf.PI);
        transform.position = new Vector3(initialX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (isMove)
        {
            caltime += Time.deltaTime;
            // ���ԂɊ�Â���x�ʒu���X�V
            float time = caltime + (startOffset * period);
            float x = _centerX + _amplitude * Mathf.Sin(2 * Mathf.PI * time / period);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
