using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalObject : MovingObject
{
    [SerializeField]
    private float moveRange = 5f;

    public override void Move()
    {
        transform.DOLocalMoveY(moveRange, movingRate / 2).OnComplete(() =>
        {
            transform.DOLocalMoveY(-(moveRange * 2), movingRate).SetDelay(delayTime).SetLoops(-1, LoopType.Yoyo);
        });
    }
}