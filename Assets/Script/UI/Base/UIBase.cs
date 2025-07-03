using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] int UINumber;

    public void Start()
    {
        //UIマネージャーに自身を登録
        UIManager.GetUIManager().RegistrationUI(this);
    }

    public void OnDestroy()
    {
        //UIマネージャーから自身を削除
        //GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().DeleteUI(this);
        UIManager UiManager = UIManager.GetUIManager();

        if (UiManager != null)
        {
            UiManager.GetComponent<UIManager>().DeleteUI(this);
        }
        else
        {
            Debug.Log("UIManagerないよ");
        }
    }

    public virtual void EraseUI()
    {
        Destroy(this.gameObject);
    }

    //UI表示
    public virtual void UIActive()
    {
        this.gameObject.SetActive(true);
    }

    //UI非表示
    public virtual void UINonActive()
    {
        this.gameObject.SetActive(false);
    }
}
