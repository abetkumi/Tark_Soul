using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SEScript : MonoBehaviour
{
    public AudioSource m_audioSource;
    public AudioClip m_stageUI_SE;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void SEPlay(AudioClip audio)
    {
        m_audioSource.PlayOneShot(audio);
    }
}
