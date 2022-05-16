using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public List<TileMoves> PosibleMoves;
    public float YMove;
    public MMF_Player MoveFeedback;
    public MMF_Player RotateFeedback;
    public MMF_Player WrongFeedback;
    public float LerpTime;
    private float time;
    private float rtime;
    bool Lift;
    private bool rotate;
    bool dir;

    public Vector3 StartPos;
    private Vector3 lerpPos;
    private Quaternion StartRot;
    private Quaternion lerpRot;


    private void OnEnable()
    {
        SetPos();

    }


    void SetPos()
    {
        StartPos = transform.position;
        lerpPos = new Vector3(transform.position.x, transform.position.y+YMove, transform.position.z);
        time = 0;
    }

    void SetRot()
    {
        StartRot = transform.rotation;
        lerpRot = Quaternion.Euler(0, StartRot.eulerAngles.y + 90, 0);
        rtime = 0;
        
        rotate = true;
    }

    private void Update()
    {
        if (Lift)
        {   

            float t = time / LerpTime;
            t = t * t * (3f - 2f * t);
            if(!dir)
                transform.position = Vector3.Lerp(StartPos, lerpPos, t); 
            if(dir)
                transform.position = Vector3.Lerp( lerpPos,StartPos, t);

            time += Time.deltaTime;

            if (time >= LerpTime)
            {
                Lift = false;
            }

        }

        if (rotate)
        {
            float t = rtime / LerpTime;
            t = t * t * (3f - 2f * t);
            
            transform.rotation = Quaternion.Lerp(StartRot,lerpRot, t);

            rtime += Time.deltaTime;

            if (rtime >= LerpTime)
            {
                rotate = false;
                transform.rotation = lerpRot;
            }

        }
    }

    
    
    
    public void WrongMove()
    {
        WrongFeedback.PlayFeedbacks();
        Debug.Log("WrongMove");

    }

    
    public void Move()
    {
        Debug.Log("Move");
        Lift = true;
        MoveFeedback.PlayFeedbacks();
    }

    public void Rotate()
    {
        Debug.Log("Rotate");
        SetRot();
        
        RotateFeedback.PlayFeedbacks();
               
    }


    public void Swap(Vector3 v)
    {
        transform.position = new Vector3(v.x,StartPos.y,v.z);
        SetPos();
    }
}
