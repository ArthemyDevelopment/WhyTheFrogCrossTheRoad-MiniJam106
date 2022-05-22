using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
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
        if (TilesManager.current.actState == GameStates.think)
        {
            TilesManager.current.actState = GameStates.frogPath;
            finish = false;
        }
    }

    private void Update()
    {

        if (!finish)
        {
            if(slerp <1)
                slerp = startTime / time;
            actPos = Vector3.Lerp(startPos, EndPos, slerp);
            actPos.y += EaseCurve.Evaluate(slerp);
            transform.position = actPos;

            startTime += Time.deltaTime;
            
            if (slerp >= 1)
            {
                
                slerp = 1;
                transform.position = EndPos;
                finish = true;
                if (TilesManager.current.actState == GameStates.frogPath)
                {
                    EndLerp();
                }
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
        if (TilesManager.current.actState != GameStates.frogPath)
            return;
        
        RaycastHit hit;

        TilePath tc;
        if (Physics.Raycast(transform.position , Vector3.down, out hit))
        {


            tc = hit.transform.gameObject.GetComponent<TilePath>();
            if (tc == null)    
            {
                if (hit.transform.gameObject.CompareTag("Finish"))
                {
                    TilesManager.current.Score++;
                    TilesManager.current.won=true;
                    StartCoroutine(TilesManager.current.EndGame());
                    return;
                }
                StartCoroutine(TilesManager.current.EndGame());
                return;

            }


            if (tc.target == null)
            {
                StartCoroutine(TilesManager.current.EndGame());
                return;
            }
   
            transform.LookAt(new Vector3(tc.target.position.x,transform.position.y,tc.target.position.z));
            SetEndPos();
            finish = false;
            return;
        }
        StartCoroutine(TilesManager.current.EndGame());


    }


}
