using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] UIPrefabList;

    List<UIBase> UIList = new List<UIBase>();

    private void Start()
    {
        NewUI(2);
    }

    public void RegistrationUI(UIBase UI)
    {
        UIList.Add(UI);
        Debug.Log("UIí«â¡");
    }

    public void DeleteUI(UIBase UI)
    {
        UIList.Remove(UI);
        Debug.Log("UIçÌèú");
    }

    public GameObject NewUI(int UINumber)
    {
        GameObject UI = Instantiate(UIPrefabList[UINumber]);
        UI.transform.SetParent(canvas.transform, false);
        return UI;
    }

    

    //UIÇîÒï\é¶
    public void NonActiveUI()
    {
        foreach (UIBase UI in UIList) 
        {
            UI.UINonActive();
        }
    }

    //UIÇï\é¶
    public void ActiveUI()
    {
        foreach (UIBase UI in UIList)
        {
            UI.UIActive();
        }
    }

    //ì¡íËÇÃUIÇï\é¶
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
            if (UI == UIPrefabList[UINumber])
            {
                UI.UIActive();
            }
        }
    }
}
