using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;

    public AudioSource musicSource;
    public AudioSource musicSource2;
    public AudioSource musicSource3;
    public AudioClip backgroundAudio;
    public AudioClip winAudio;
    public AudioClip deathAudio;

    private bool isDead;
    private int scoreValue;

    void Start()
    {
        musicSource.clip = backgroundAudio;
        musicSource.Play();
        musicSource2.clip = winAudio;
        musicSource3.clip = deathAudio;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        isDead = PlayerController.isDead;
        scoreValue = PlayerController.scoreValue;
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);

        if (scoreValue == 8)
        {
            if (musicSource.volume == .25)
            {
                musicSource.volume = 0;
                musicSource2.Play();
            }
        }

        if (isDead == true)
        {
            if (musicSource.volume == .25)
            {
                musicSource.volume = 0;
                musicSource3.Play();
            }
        }
    }
}
