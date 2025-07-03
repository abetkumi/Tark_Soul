using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUIBase : UIBase
{
    private float _timeLimit = 0.0f;
    private bool _isAutoDelete = false;

    void Start()
    {
        base.Start();
    }

    //©“®íœ‚ğİ’è
    public void SetAutoDelete(float time)
    {
        _timeLimit = time;
        _isAutoDelete = true;
    }

    private void Update()
    {
        if(_isAutoDelete)
        {
            _timeLimit -= Time.deltaTime;
            if(_timeLimit < 0.0f)
            {
                EraseUI();
            }
        }
    }


}
