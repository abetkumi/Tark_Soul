using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] int UINumber;

    public void Start()
    {
        //UIマネージャーに自身を登録
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().RegistrationUI(this);
    }

    public void OnDestroy()
    {
        //UIマネージャーから自身を削除
        //GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().DeleteUI(this);
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");

        if(UIManager != null)
        {
            UIManager.GetComponent<UIManager>().DeleteUI(this);
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
