using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalObject : MovingObject
{
    [SerializeField]
    private float moveRange = 5f;

    public override void Move()
    {
        transform.DOLocalMoveX(moveRange, movingRate / 2).OnComplete(() =>
        {
            transform.DOLocalMoveX(-(moveRange * 2), movingRate).SetDelay(delayTime).SetLoops(-1, LoopType.Yoyo);
        });
    }
}