using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public GameObject msgPanel;
    public float scopedFOV = 40f;
    private float normalFOV = 60f;
    public Camera mainCamera;
    private bool isScoped = false;

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);

            if(isScoped)
                StartCoroutine(OnScoped());
            else
                OnUnscoped();
        }    
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = normalFOV;
        msgPanel.SetActive(true);
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        msgPanel.SetActive(false);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        mainCamera.fieldOfView = scopedFOV;
    }
}
