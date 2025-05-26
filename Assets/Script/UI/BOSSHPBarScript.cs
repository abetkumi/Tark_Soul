using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSSHPBarScript : MonoBehaviour
{
    BOSSEnemyScript m_BOSSEnemyScript;
    [SerializeField] GameObject m_BOSSEnemyObject;

    private int _MaxHP;
    private int _currentHP;   //現在HP
    private Slider _BOSShpSlider;

    // Start is called before the first frame update
    void Start()
    {
        m_BOSSEnemyScript = m_BOSSEnemyObject.GetComponent<BOSSEnemyScript>();
        _BOSShpSlider = GetComponent<Slider>();
        gameObject.SetActive(false);
    }

    //
    public void Init(int MaxHP)
    {
        _MaxHP = MaxHP;
        _BOSShpSlider.maxValue = _MaxHP;  //スライダーの最大値を設定
        _currentHP = _MaxHP;
        _BOSShpSlider.value = _currentHP; //現在のHPを反映

    }

    public void HPUpdate(int nowHP)
    {
        //現在HPを更新
        _currentHP = nowHP;
        if (_currentHP < 0)
        {
            _currentHP = 0;
        }

        //スライダーに反映
        _BOSShpSlider.value = _currentHP;
    }
}
