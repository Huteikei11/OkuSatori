using System.Collections;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public float holdTimeToDodge = 2f; // Inspector で設定可能
    public bool isDodging = false;
    private float rightClickHoldTime = 0f;

    public SatoriController satoriController = null;
    public DynamicQuadGenerator dynamicQuadGenerator;
    public OscillateSprite oscillateSprite;

    void Update()
    {
        if (Input.GetMouseButton(1)) // 右クリックが押されている
        {
            rightClickHoldTime += Time.deltaTime;
            if (!isDodging && rightClickHoldTime >= holdTimeToDodge)
            {
                StartDodge();
                rightClickHoldTime = 0f; // カウントをリセット
            }
            else if (isDodging && rightClickHoldTime >= holdTimeToDodge)
            {
                StopDodge();
                rightClickHoldTime = 0f; // カウントをリセット
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
        dynamicQuadGenerator.isMove = !isDodging; //回避中はセクハラできない
        oscillateSprite.isMove = !isDodging;//回避中はバーを止める
        satoriController.StartDodge();
        Debug.Log("回避状態に入りました");
    }

    public void StopDodge()
    {
        isDodging = false;
        dynamicQuadGenerator.isMove = !isDodging;//セクハラ許可再開
        oscillateSprite.isMove= !isDodging;//バー移動再開
        satoriController.StopDoge();
        Debug.Log("回避状態を解除しました");
    }
}
