using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class goodbad : MonoBehaviour
{
    [SerializeField] SpriteResolver resolver;
    public int type;
    // Start is called before the first frame update

    public int PopJuge(int type)
    {
        if (type == 0)
        {
            resolver.SetCategoryAndLabel("good", "none");
        }
        if (type == 1)
        {
            resolver.SetCategoryAndLabel("good", "good");
        }
        if (type == 2) 
        {
            resolver.SetCategoryAndLabel("good", "bad");
        }
        return type;
    }
}