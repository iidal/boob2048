using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBar : MonoBehaviour
{
    float timer;
    public bool touching = false;

    [SerializeField] Animator gameAreaAnim;

    void Start()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {

        touching = true;
        if (other.tag.Contains("boob"))
        {
            StartCoroutine("CheckDeathBar");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Contains("boob"))
        {
            touching = false;
        }
    }

    IEnumerator CheckDeathBar()
    {
        yield return new WaitForSeconds(0.5f);
        gameAreaAnim.SetBool("CheckingDeath", true);
        while (touching == true)
        {
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
            
                BoobMG.instance.GameOver();
                this.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);

        }
        timer = 0;
        gameAreaAnim.SetBool("CheckingDeath", false);

        yield return null;
    }
}
