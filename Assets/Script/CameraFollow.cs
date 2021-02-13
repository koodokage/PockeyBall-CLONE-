using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private GameObject ball;
    [SerializeField]private GameObject player;
    private Vector3 offsetZoom;
    private float rotCalcul;
    [Header("Settings")]
    [SerializeField] Vector3 mOffset;
    private void Start()
    {
        offsetZoom.x = mOffset.x;
        rotCalcul = -0.20f;
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("PlayerBall");
        transform.position = ball.transform.position + mOffset ;
        if(player.transform.rotation.z <= rotCalcul)
        {
              
            if (mOffset.x < 4)
                mOffset.x += Time.deltaTime*4;
            

        }
        else if(mOffset.x >= offsetZoom.x)
        {
                mOffset.x -= Time.deltaTime*2;
          
        }






    }

   
}
