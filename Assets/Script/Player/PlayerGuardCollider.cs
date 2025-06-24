using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardCollider : MonoBehaviour
{
    GameObject _player;
    PlayerScript _playerScript;

    private void Start()
    {
        _player = transform.parent.gameObject;
        _playerScript = _player.GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //コリジョンの持ち主がエネミーか調べる
        if (!collision.gameObject.transform.root.CompareTag("Enemy"))
        {
            return;
        }

        //無敵時間中なら処理をしない
        if (_playerScript.IsInvincible())
        {
            return; 
        }

        _playerScript.StartInvincibleTime(2.0f);
        _playerScript.SetPlayerState(new PlayerStateGuardImpact(_player));

        Debug.Log(collision);
    }
}
