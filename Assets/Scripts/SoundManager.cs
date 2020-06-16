using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip putSound, leaveSound, grabSound;
    static AudioSource audioSrc;
    void Start()
    {

        putSound = Resources.Load<AudioClip>("put");
        leaveSound = Resources.Load<AudioClip>("leave");
        grabSound = Resources.Load<AudioClip>("grab");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = 0.3f;

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "put":
                audioSrc.PlayOneShot(putSound);
                break;
            case "leave":
                audioSrc.PlayOneShot(leaveSound);
                break;
            case "grab":
                audioSrc.PlayOneShot(grabSound);
                break;
        }
    }
}
