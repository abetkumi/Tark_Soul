using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] Button m_focusButton_Start;
    [SerializeField] Button m_focusButton_End;
    [SerializeField] Image m_loadingObject;
    [SerializeField] TextMeshProUGUI m_startButtonTextUI;
    [SerializeField] TextMeshProUGUI m_endButtonTextUI;
    [SerializeField] TextMeshProUGUI m_loadingTextUI;
    bool m_startLoading = false;
    float t = 0.0f;
    float m_loadtext = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_startLoading = false;
        t = 0.0f;
        m_loadtext = 0.0f;
        m_focusButton_Start = m_focusButton_Start.GetComponent<Button>();
        m_focusButton_End = m_focusButton_End.GetComponent<Button>();
        m_loadingObject.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        m_loadingTextUI.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        m_focusButton_Start.Select();

    }

    // ボタンが押された場合、今回呼び出される関数
    async public void OnClickStartButton(string sceneName)
    {
        if (!m_startLoading)
        {
            m_startLoading = true;
            EventSystem.current.SetSelectedGameObject(null);
            m_focusButton_Start.Select();
            await UniTask.Delay(3000);
            //メインゲームシーンに移動する
        
            Debug.Log("ゲームスタート!");  // ログを出力
        }

    }

    public void OnClickEndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    void doLoading()
    {
        //ボタンの透明度を徐々に減らす変数
        t += Time.deltaTime;
        //
        m_loadtext += Time.deltaTime / 2.0f;

        m_startButtonTextUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - t);
        m_endButtonTextUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - t);
        m_loadingObject.color = new Color(0.0f, 0.0f, 0.0f, t);
        m_loadingTextUI.color = new Color(1.0f, 1.0f, 1.0f, m_loadtext);
        if (t > 1.0f)
        {
            t = 1.0f;
            m_focusButton_Start.gameObject.SetActive(false);
        }
        if (m_loadtext > 1.0f)
        {
            m_loadtext = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_startLoading)
        {
            doLoading();
        }
    }
}
