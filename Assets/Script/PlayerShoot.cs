using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    

    public event EventHandler<OnShootEventArg> OnShoot;
    public class OnShootEventArg : EventArgs
    {
        public Vector3 gunEndPoint;
        public Vector3 gunAimPoint;
        public Vector3 meshLocation;
    }
    public Camera cam;
    public GameObject aim;
    private Animator aimAnim;
    private Transform aimTransform;
    private Transform gunEndpointTransform;
    private Transform meshLocationTransform;
    Vector2 mousepos;

    public void Awake()
    {
        
        aimAnim = gameObject.GetComponent<Animator>();
        aimTransform = transform.Find("AimGun");
        gunEndpointTransform = aimTransform.Find("GunEndPoint");
        meshLocationTransform = aimTransform.Find("meshLocation");
        
    }

    void Update()
    {
        HandleAim();
       
        HandleShoot();
    }

    private void HandleAim()
    {
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousepos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        aim.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void HandleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
        aimAnim.SetTrigger("shoot");
            
        OnShoot?.Invoke(this, new OnShootEventArg {
            gunEndPoint=gunEndpointTransform.position,
            gunAimPoint = mousepos,
            meshLocation = meshLocationTransform.position,
        });
        }



    }
}
