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
        if (collision.CompareTag("Player"))
        {
            return;
        };

        if(!_playerScript.IsInvincible())
        {
            _playerScript.StartInvincibleTime(2.0f);
            _playerScript.SetPlayerState(new PlayerStateGuardImpact(_player));
        }
    }
}
