using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーステートの基底クラス
public class IPlayerStateScript
{
    public virtual void Start() { }

    public virtual void End() { }

    public virtual void Update() { }

    public virtual void AnimationEvent(string EventName) { }
}
