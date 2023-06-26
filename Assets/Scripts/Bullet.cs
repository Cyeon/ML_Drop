using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    private Rigidbody rigid = null;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void Fire(Vector3 target)
    {
        target.y = transform.position.y;
        Vector3 dir = target - transform.position;
        transform.DOMove(dir * 20, 20f);
    }

    public void RigidReset()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        rigid.Sleep();
        rigid.WakeUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.collider.CompareTag("WALL") || collision.collider.CompareTag("DEAD") || collision.collider.CompareTag("Player"))
        {
            transform.DOKill();
            BulletPool.Instance.Push(this);
        }
    }
}