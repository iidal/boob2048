using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittyBgManager : MonoBehaviour
{
    [SerializeField] Material bgMat;

    float f = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        f += Time.deltaTime * 0.2f;
        bgMat.SetTextureOffset("_MainTex", new Vector2(f, 0));
    }
}
