using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MovingObject
{
    public override void Move()
    {
        transform.DORotate(new Vector3(0, 360, 0), movingRate, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetDelay(delayTime).SetLoops(-1);
    }
}