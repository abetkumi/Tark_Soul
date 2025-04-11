using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー用スクリプトクラス
public class PlayerScript : MonoBehaviour
{
    //軸入力用変数
    float vert, horiz;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doMove();
    }

    //移動用メソッド
    void doMove()
    {
        //Lスティックの縦横の入力を取得
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        //カメラの正面ベクトルから、横(x.z)方向のベクトルを抽出し正規化
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Lスティックの入力とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vert + Camera.main.transform.right * horiz;

        //移動方向にプレイヤーを動かす
        transform.Translate(moveForward * 5.0f * Time.deltaTime);
    }
}
