using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatoriController : MonoBehaviour
{
    private Animator anim = null;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("SatoriState", state);
    }

    public void SetState(int getstate)
    {
        state = getstate;
    }

    public int Getstate() 
    { 
        return state;
    }

    public void StartDodge()
    {
        int set;

        //ランダムで決まる
        set = 5; //後で作る

        SetState(set);
    }
    public void StopDoge() 
    {
    //射精度を参照
    SetState(0); //後で作る
    }
}
