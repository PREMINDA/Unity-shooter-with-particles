using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;

    void Start()
    {
        playerShoot.OnShoot += PlayerShoot_OnShoot;
    }

    private void PlayerShoot_OnShoot(object sender, PlayerShoot.OnShootEventArg e)
    {
        Debug.DrawLine(e.gunEndPoint, e.gunAimPoint);
        CamShake.Instance.ShakeCamer(5f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
