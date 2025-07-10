using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class ClearScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_bornfire;

    //クリア演出（仮）
    [SerializeField]
    GameObject m_clearText;
    bool m_titleBack = false;
    [SerializeField]
    GameObject m_fadeCanvas;
    bool m_sceneChange = false;
    //BGM切り替え
    [SerializeField]
    AudioClip m_clip;
    AudioSource m_audioSource;
    //BGMを止める用
    [SerializeField]
    GameObject m_bgmObject;
    BGMScript m_bgmScript;
    //SEを止める用
    [SerializeField]
    AudioSource m_playerAudio;


    // Start is called before the first frame update
    void Start()
    {
        m_bornfire.SetActive(false);
        m_bgmScript = m_bgmObject.GetComponent<BGMScript>();
        m_audioSource = GetComponent<AudioSource>();
    }

    public void ClearActive()
    {
        m_bornfire.SetActive(true);
    }

    async public void Clear()
    {
        //UIを非表示にする
        UIManager.GetUIManager().NonActiveUI();

        //クリア演出
        m_clearText.SetActive(true);
        await UniTask.Delay(1000);
        m_titleBack = true;
        GameObject fadeObject = m_fadeCanvas;
        fadeObject.GetComponent<ClearFadeScript>().FadeStart(Color.red, false);

        //BGMストップ
        m_bgmScript.BGMStop();
        m_audioSource.PlayOneShot(m_clip);

        //プレイヤー削除
        await UniTask.Delay(1000);
        m_playerAudio.enabled = false;
        Debug.Log("クリア");
    }

    // Update is called once per frame
    async void Update()
    {
        if (m_titleBack == true && Input.GetButtonDown("Action"))
        {
            //タイトルシーンに移動する
            await SceneManager.LoadSceneAsync("Title").ToUniTask();
        }
    }
}
