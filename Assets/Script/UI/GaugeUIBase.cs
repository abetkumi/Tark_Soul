using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeUIBase : UIBase
{
    public void Start()
    {
        base.Start();
    }

    //ゲージの減少
    public virtual void IncreaseGuage(float value)
    {

    }

    //ゲージの増加
    public virtual void DecreaseGauge(float value)
    {

    }

    //最大値を設定
    public virtual void SetMaxValue(float value)
    {

    }

    //最大値を取得
    public virtual float GetMaxValue()
    {
        return 0.0f;
    }

    //現在の値を設定
    public virtual void SetCurrentValue(float value)
    {

    }

    //現在の値を取得
    public virtual float GetCurrentValue()
    {
        return 0.0f;
    }
}
