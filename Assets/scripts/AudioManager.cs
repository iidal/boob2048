using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   

    public static AudioManager instance;
    [SerializeField] AudioSource boobsAS;
    [SerializeField] AudioClip[] boobsTouchingClips;
    [SerializeField] AudioClip matchClip;
    bool playingSound = false;
    bool playingMatchSound = false;
    void Start()
    {
        if(instance != null){ Destroy(this);}
        else{instance = this;}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBoobSoundEffect(){
        StartCoroutine("PlaySound");
    }
    IEnumerator PlaySound(){

        if(playingSound ==false){
            playingSound = true;
            boobsAS.PlayOneShot(boobsTouchingClips[Random.Range(0, boobsTouchingClips.Length)], 0.5f);
            yield return new WaitForSeconds(0.7f);
            playingSound = false;
        }

    }
    public void PlayMatchSound(){
        StartCoroutine("PlayMatchedSoundEffect");
    }
       IEnumerator PlayMatchedSoundEffect(){

        if(playingMatchSound ==false){
            playingMatchSound = true;
            boobsAS.PlayOneShot(matchClip, 1f);
            yield return new WaitForSeconds(0.4f);
            playingMatchSound = false;
        }

    }
}
