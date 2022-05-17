using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Vector3 EndPos;
    float time;
    public AnimationCurve EaseCurve;

    
    private Vector3 startPos;
    private float startTime;
    private bool finish=true;
    private Vector3 actPos;
    float slerp = 0;

    private void Start()
    {
        time = TilesManager.current.timeScale;
        SetEndPos();
    }

    public void StartMovement()
    {

        finish = false; 
    }

    private void Update()
    {

        if (!finish)
        {
            if(slerp <1)
                slerp = startTime / time;
            actPos = Vector3.Lerp(startPos, EndPos, slerp);
            //actPos.y += EaseCurve.Evaluate(slerp);
            transform.position = actPos;

            startTime += Time.deltaTime;
            
            if (slerp >= 1)
            {
                
                slerp = 1;
                transform.position = EndPos;
                finish = true;
                EndLerp();
            }
        }
    }



    void SetEndPos()
    {
        startPos = transform.position;
        EndPos = transform.position + transform.forward;
        startTime = 0;
        slerp = 0;
    }

    void EndLerp()
    {

        if (TilesManager.current.actState == GameStates.frogPath)
        {
            RaycastHit hit;

            carPath tc;
            if (Physics.Raycast(transform.position , Vector3.down, out hit))
            {
                tc = hit.transform.gameObject.GetComponent<carPath>();
                transform.LookAt(new Vector3(tc.target.position.x,transform.position.y,tc.target.position.z));
                SetEndPos();
                finish = false;

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(TilesManager.current.EndGame());
    }
}
