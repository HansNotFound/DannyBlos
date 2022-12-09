using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletParent;
    public float fireRate = 1f;
    private float nextFireTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2) 
        {
            timer = 0;
            Shoot();
        }
    }
    private void Shoot()
    {
        Debug.Log("Enemy Shoot");     
        nextFireTime = Time.time + fireRate;
        Instantiate(bullet, bulletParent.position, Quaternion.identity);
    }
}
