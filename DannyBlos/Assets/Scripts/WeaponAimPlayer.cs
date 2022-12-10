using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
public class WeaponAimPlayer : MonoBehaviour
{
    private Transform aimTransform;
    private Animator aimAnimator;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float fireRate = 0.5f;
    private float nextFire = 0f;

    private float reloadTime = 1f;

    private bool isReloading = false;
    public int ammo = 5;
    public TMP_Text ammoText;

    private void Awake()
    {   
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
    }
    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming() 
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

    }

    private void HandleShooting() 
    {
        if (isReloading) 
        {
            return;
        }
        if (ammo <= 0) 
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire) 
        {
            //aimAnimator.SetTrigger("Shoot");
            Debug.Log("Pressed Left Click");
            Shoot();
        }

    }

    private void Shoot() {
        ammo--;
        ammoText.text = ammo.ToString();
        nextFire =Time.time + fireRate;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator Reload() {
        isReloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammo = 5;
        isReloading = false;
    }
}
