using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{

    Animator fadeAnim;
    public UnityEvent afterFade;
    [SerializeField] Image blackImg;
    [SerializeField] Color fadeColor;
    // Start is called before the first frame update
    void OnEnable()
    {
        fadeAnim = GetComponent<Animator>();
        fadeAnim.Play("FadeIn");
    }

    public void FadeOut(){
        StartCoroutine("FadingOut");
    }
    IEnumerator FadingOut(){

        fadeAnim.Play("FadeOut");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(()=> fadeAnim.GetCurrentAnimatorStateInfo(0).IsName("Black"));
        blackImg.color = fadeColor;
        afterFade.Invoke();
    }


}
