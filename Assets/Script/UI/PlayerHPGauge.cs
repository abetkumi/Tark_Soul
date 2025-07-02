using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーのHPバースクリプト
public class PlayerHPGauge : GaugeUIBase
{
    private float _maxHP;
    private float _currentHP;   //現在HP

    private Slider _hpSlider;



    public void Init(int MaxHP)
    {
        Debug.Log(MaxHP);

        _hpSlider = GetComponent<Slider>();

        _maxHP = MaxHP;
        _hpSlider.maxValue = _maxHP;  //スライダーの最大値を設定

        _currentHP = _maxHP;
        _hpSlider.value = _currentHP; //現在のHPを反映

    }

    public override void  IncreaseGuage(float value)
    {
        _currentHP += value;

        GaugeUpdate();
    }
    public override void DecreaseGauge(float value)
    {
        _currentHP -= value;

        GaugeUpdate();
    }

    public override void SetCurrentValue(float value)
    {
        _currentHP = value;

        GaugeUpdate();
    }

    public override float GetCurrentValue()
    {
        return _currentHP;
    }

    public override void SetMaxValue(float value)
    {
        _hpSlider.maxValue = value;
    }



    public void GaugeUpdate()
    {
        //スライダーに反映
        _hpSlider.value = _currentHP;
    }
}
