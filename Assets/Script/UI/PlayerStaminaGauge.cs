using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaGauge : GaugeUIBase
{
    private float _maxStamina;
    private float _currentStamina;   //現在スタミナ量

    private Slider _StaminaSlider;

    private bool _UnRecoverable;    //スタミナ回復不能フラグ、主にスタミナが切れた時に少しの間回復しないようにする

    public void Init(int MaxStamina)
    {
        Debug.Log(MaxStamina);

        _StaminaSlider = GetComponent<Slider>();

        _maxStamina = MaxStamina;
        _StaminaSlider.maxValue = _maxStamina;  //スライダーの最大値を設定

        _currentStamina = _maxStamina;
        _StaminaSlider.value = _currentStamina; //現在のスタミナを反映
    }

    public override void IncreaseGuage(float value)
    {
        if(_UnRecoverable)
        {
            return;
        }

        _currentStamina += value;

        if (_currentStamina > _maxStamina)
        {
            _currentStamina = _maxStamina;
        }

        GaugeUpdate();
    }
    public override void DecreaseGauge(float value)
    {
        _currentStamina -= value;

        if (_currentStamina < 0 && _UnRecoverable == false)
        {
            _currentStamina = 0;
            _UnRecoverable = true;
            SetRecoverable(2000);
        }


        GaugeUpdate();
    }

    async private void SetRecoverable(int StartRecoverableTime)
    {
        await UniTask.Delay(StartRecoverableTime);
        _UnRecoverable = false;
    }


    public override void SetCurrentValue(float value)
    {
        _currentStamina = value;

        GaugeUpdate();
    }

    public override float GetCurrentValue()
    {
        return _currentStamina;
    }

    public override void SetMaxValue(float value)
    {
        _StaminaSlider.maxValue = value;
    }



    public void GaugeUpdate()
    {
    //スライダーに反映
    _StaminaSlider.value = _currentStamina;
    }
}
