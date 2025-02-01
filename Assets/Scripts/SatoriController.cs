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

        //ƒ‰ƒ“ƒ_ƒ€‚ÅŒˆ‚Ü‚é
        set = 5; //Œã‚Åì‚é

        SetState(set);
    }
    public void StopDoge() 
    {
    //Ë¸“x‚ğQÆ
    SetState(0); //Œã‚Åì‚é
    }
}
