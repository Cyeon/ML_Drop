using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRot = Quaternion.identity;

    [SerializeField]
    protected float movingRate = 1f;
    [SerializeField]
    protected float delayTime = 0f;

    public void Init()
    {
        originPos = transform.localPosition;
        originRot = transform.rotation;
    }

    public void ObjectStop()
    {
        transform.DOKill();
        transform.localPosition = originPos;
        transform.rotation = originRot;
    }

    public abstract void Move();
}