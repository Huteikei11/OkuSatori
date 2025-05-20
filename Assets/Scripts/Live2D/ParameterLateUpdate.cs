using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class ParameterLateUpdate : MonoBehaviour
{
    private CubismModel _model;
    private float _t;
    public int num;

    private void Start()
    {
        _model = this.FindCubismModel();
    }

    private void LateUpdate()
    {
        _t += (Time.deltaTime * 4f);
        var value = Mathf.Sin(_t) * 30f;

        var parameter = _model.Parameters[num];

        parameter.Value = value;
    }
}