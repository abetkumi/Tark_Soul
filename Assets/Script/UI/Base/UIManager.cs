using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static private UIManager _UIManager;
    static private Canvas _Canvas;
    static private List<GameObject> _UIPrefabList = new List<GameObject>();


    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] UIPrefab;

    List<UIBase> UIList = new List<UIBase>();

    private void Awake()
    {
        if(_UIManager == null)
        {
            _UIManager = new UIManager();
            _UIManager.Init(canvas, UIPrefab);
        }
    }

    private void Init(Canvas canvas, GameObject[] UIPrefab)
    {
        _Canvas = canvas;
        _UIPrefabList.AddRange(UIPrefab);
    }


    private void Start()
    {
        _UIManager.NewUI(2);
    }

    //UIManagerを取得
    static public UIManager GetUIManager()
    {
        return _UIManager;
    }

    public void RegistrationUI(UIBase UI)
    {
        UIList.Add(UI);
        Debug.Log("UI追加");
    }

    public void DeleteUI(UIBase UI)
    {
        UIList.Remove(UI);
        Debug.Log("UI削除");
    }

    public GameObject NewUI(int UINumber)
    {
        GameObject UI = Instantiate(_UIPrefabList[UINumber]);
        UI.transform.SetParent(_Canvas.transform, false);
        return UI;
    }

    

    //UIを非表示
    public void NonActiveUI()
    {
        foreach (UIBase UI in UIList) 
        {
            UI.UINonActive();
        }
    }
    //特定のUIを非表示
    public void NonActiveUI(string UIName)
    {
        foreach (UIBase UI in UIList)
        {
            if (UI.gameObject.name == UIName)
            {
                UI.UINonActive();
            }
        }
    }
    public void NonActiveUI(int UINumber)
    {
        foreach (UIBase UI in UIList)
        {
            if (UI == _UIPrefabList[UINumber])
            {
                UI.UINonActive();
            }
        }
    }


    //UIを表示
    public void ActiveUI()
    {
        foreach (UIBase UI in UIList)
        {
            UI.UIActive();
        }
    }

    //特定のUIを表示
    public void ActiveUI(string UIName)
    {
        foreach(UIBase UI in UIList)
        {
            if(UI.gameObject.name == UIName)
            {
                UI.UIActive();
            }
        }
    }
    public void ActiveUI(int UINumber)
    {
        foreach (UIBase UI in UIList)
        {
            if (UI == _UIPrefabList[UINumber])
            {
                UI.UIActive();
            }
        }
    }
}
