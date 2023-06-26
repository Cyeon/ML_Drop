using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance = null;
    public static BulletPool Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Instance NULL");
            }
            return instance;
        }
    }


    public GameObject bulletPrefab = null;

    private List<Bullet> bulletList = new List<Bullet>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        CreateBullet(50);
    }

    public void CreateBullet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            Push(bullet.GetComponent<Bullet>());
        }
    }

    public void Push(Bullet bullet)
    {
        bullet.transform.SetParent(transform);
        bullet.gameObject.SetActive(false);
        bulletList.Add(bullet);
    }

    public Bullet Pop()
    {
        if (bulletList.Count == 0)
            CreateBullet(10);

        Bullet bullet = bulletList[bulletList.Count - 1];
        bulletList.Remove(bullet);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
}