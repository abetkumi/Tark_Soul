using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー用スクリプトクラス
public class PlayerScript : MonoBehaviour
{
    
    private float vert, horiz;  //軸入力用変数
    private CharacterController characterController;    //プレイヤー用キャラクターコントローラ


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
        characterController.Move(moveForward * 3.0f * Time.deltaTime);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
}
