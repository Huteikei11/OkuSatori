using UnityEngine;

public class OscillateSprite : MonoBehaviour
{
    [Header("振動設定")]
    [Tooltip("振幅の右端の座標")]
    public float rightBound = 3.0f;

    [Tooltip("振幅の左端の座標")]
    public float leftBound = -3.0f;

    [Tooltip("振動の周期 (秒)")]
    public float period = 2.0f;

    [Tooltip("開始時の位置 (0 ~ 1)")]
    [Range(0f, 1f)]
    public float startOffset = 0.0f;

    private float _amplitude; // 振幅
    private float _centerX;   // 中心位置

    public bool isMove;
    private float caltime;

    void Start()
    {
        // 振幅と中心位置を計算
        _amplitude = (rightBound - leftBound) / 2.0f;
        _centerX = (rightBound + leftBound) / 2.0f;

        // 開始位置を設定
        float initialX = _centerX + _amplitude * Mathf.Sin(startOffset * 2 * Mathf.PI);
        transform.position = new Vector3(initialX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (isMove)
        {
            caltime += Time.deltaTime;
            // 時間に基づいてx位置を更新
            float time = caltime + (startOffset * period);
            float x = _centerX + _amplitude * Mathf.Sin(2 * Mathf.PI * time / period);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
