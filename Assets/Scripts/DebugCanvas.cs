using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField]Å@private KeikaiManager keikaiManager;

    [SerializeField] private TextMeshProUGUI alert;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        alert.text = "Alert:" +keikaiManager.alertLevel.ToString();
    }
}
