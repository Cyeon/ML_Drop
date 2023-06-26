using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float delayTime = 2f;
    public Transform firePosition = null;
    private Transform target = null;
    private bool isFire = false;

    private void Start()
    {
        target = transform.parent.Find("LocalZero");
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            if (!isFire)
                yield break;

            Bullet bullet = BulletPool.Instance.Pop();
            bullet.transform.SetParent(transform);
            bullet.transform.position = firePosition.position;
            bullet.RigidReset();
            bullet.Fire(target.position);
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void SetFire(bool flag)
    {
        isFire = flag;
    }
}