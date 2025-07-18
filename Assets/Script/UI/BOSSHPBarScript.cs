using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSSHPBarScript : GaugeUIBase
{

    private int _MaxHP;
    private float _currentHP;   //現在HP
    private Slider _BOSShpSlider;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _BOSShpSlider = GetComponent<Slider>();
        gameObject.SetActive(false);
        _MaxHP = 20;
        Init(_MaxHP);
    }

    public void Init(int MaxHP)
    {
        Debug.Log(MaxHP);

        _BOSShpSlider = GetComponent<Slider>();

        _MaxHP = MaxHP;
        _BOSShpSlider.maxValue = _MaxHP;  //スライダーの最大値を設定

        _currentHP = _MaxHP;
        _BOSShpSlider.value = _currentHP; //現在のHPを反映

    }

    public override void IncreaseGuage(float value)
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
        _BOSShpSlider.maxValue = value;
    }



    public void GaugeUpdate()
    {
        //スライダーに反映
        _BOSShpSlider.value = _currentHP;
    }
}
