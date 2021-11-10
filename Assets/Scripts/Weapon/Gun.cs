using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;
    public Camera fpsCam;
    public GameObject muzzleFlash;
    public GameObject impactEffect;
    public float impactForce;
    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;
    public Animator animator;
    void Start() {
            currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        if(isReloading)
            return;
        if(currentAmmo<=0)
        {
            FindObjectOfType<AudioManager>().PlaySound("Reload");
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {

        muzzleFlash.SetActive(true);
        FindObjectOfType<AudioManager>().PlaySound("Shoot");
        StartCoroutine(wait());
        currentAmmo --;
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            if (target!=null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.2f);
        }
    }

    IEnumerator wait()
    {
       yield return new WaitForSeconds(0.12f);
       muzzleFlash.SetActive(false);
    }
}
