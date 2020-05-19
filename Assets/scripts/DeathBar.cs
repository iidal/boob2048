using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBar : MonoBehaviour
{
    public float timer;
    bool touching = false;
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

        while (touching == true)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                Debug.Log("gameover");
                BoobMG.instance.GameOver();
                this.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);

        }
        timer = 0;

        yield return null;
    }
}
