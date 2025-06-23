using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    public AudioSource m_audioSource;
    public AudioClip m_stageAudioClip;
    public AudioClip m_bossAudioClip;
    float t = 0.1f;
    bool m_bgmStop = false;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        BGMPlay(m_stageAudioClip);
    }

    public void BGMPlay(AudioClip audio)
    {
        m_audioSource.PlayOneShot(audio);
    }

    public void BGMStop()
    {
        m_bgmStop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bgmStop == true)
        {
            m_audioSource.volume -= Time.deltaTime * 0.8f;
            if (m_audioSource.volume <= 0.1f)
            {
                m_audioSource.Stop();
                m_bgmStop = false;
                m_audioSource.volume = 1.0f;
            }
        }

    }
}
