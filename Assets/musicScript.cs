 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class musicScript : MonoBehaviour
 {
    int index = 0;
    bool playing = false;
    private float timer = 0;
    private float timerAtack = 0;
    private AudioSource track;
 
     void Start ()
     {
        timerAtack = timer;
     }
     
     void Update(){
            timer += Time.deltaTime;
            var asources = GetComponents<AudioSource>();
            playing = false;
            for(var i=0;i<8;i++){
                if(asources[i].isPlaying)
                    playing = true;
            }
             if (!playing)
             {
                if (timerAtack + 0.5 < timer)
                        {
                            Debug.Log("play next");
                            PlayMusic();
                        }
             }
         }

    void PlayMusic(){
        timerAtack = timer;
        var asources = GetComponents<AudioSource>();
        index = Random.Range(0, 8);
        track = asources[index];
        track.volume = 0.1f;
        track.Play();
        }
 }