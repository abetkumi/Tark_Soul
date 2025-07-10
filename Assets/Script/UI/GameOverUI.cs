using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    Image _image;
    UnityEngine.Color _color;
    float FadeSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    // Update is called once per frame
    void Update()
    {
        _color.a += FadeSpeed * Time.deltaTime;

        if (_color.a < 0)
        {
            Debug.Log("フェードアウト完了");
            _color.a = 0;
            FadeSpeed = 0.0f;
        }

        if (_color.a > 1)
        {
            Debug.Log("フェードイン完了");
            _color.a = 1;
            FadeSpeed = 0.0f;
        }

        _image.color = _color;
    }

    //フェードインさせる
    public void FadeIn()
    {
        FadeSpeed = 1.0f;
    }

    //フェードアウトさせる
    public void FadeOut()
    {
        FadeSpeed = -1.0f;
    }
}
