using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーのHPバースクリプト
public class PlayerHPBar : MonoBehaviour
{
    private int _MaxHP;
    private int _currentHP;   //現在HP

    private Slider _hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        _hpSlider = GetComponent<Slider>();
        
    }

    public void Init(int MaxHP)
    {
        _MaxHP = MaxHP;
        _hpSlider.maxValue = _MaxHP;  //スライダーの最大値を設定
        _currentHP = _MaxHP;
        _hpSlider.value = _currentHP; //現在のHPを反映

    }

    public void HPUpdate(int nowHP)
    {
        //現在HPを更新
        _currentHP = nowHP;
        if(_currentHP < 0)
        {
            _currentHP = 0;
        }

        //スライダーに反映
        _hpSlider.value = _currentHP;
    }
}
