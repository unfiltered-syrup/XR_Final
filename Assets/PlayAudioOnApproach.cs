using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnApproach : MonoBehaviour
{
    // Start is called before the first frame update
    
        public Transform player;
        public Transform target;
        public AudioSource audioSource;
        public AudioSource audioSource2;
        public AudioClip clip;

        public AudioClip clip2;
        public float triggerRange = 10f;

        private bool isAudioPlayed = false;
    void Start()
        {
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(player.position, target.position);

        if (distanceToTarget <= triggerRange && !isAudioPlayed)
        {
            audioSource.clip = clip;
            audioSource.Play();
            audioSource2.clip = clip2;
            audioSource2.Play();
            isAudioPlayed = true; // Prevents audio from playing repeatedly
        }
        else if (distanceToTarget > triggerRange)
        {
            isAudioPlayed = false; // Resets the condition when target moves out of range
        }
        }
}
