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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClearActive()
    {
        m_bornfire.SetActive(true);
    }

    async public void Clear()
    {
        m_clearText.SetActive(true);

        await UniTask.Delay(1000);
        m_titleBack = true;
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
