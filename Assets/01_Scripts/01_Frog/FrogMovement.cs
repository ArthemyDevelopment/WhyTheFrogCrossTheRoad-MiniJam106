using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    public Vector3 EndPos;
    float time;
    public AnimationCurve EaseCurve;
    private Animator frogAnim;

    public float MidleStopTime;
    
    private Vector3 startPos;
    private float startTime;
    private bool finish=true;
    private Vector3 actPos;
    float slerp = 0;

    private void Start()
    {
        time = TilesManager.current.timeScale;
        frogAnim = GetComponent<Animator>();
        SetEndPos();
    }

    public void StartMovement()
    {
        if (TilesManager.current.actState == GameStates.think)
        {
            TilesManager.current.actState = GameStates.frogPath;
            frogAnim.SetTrigger("StartJump");
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

            if (slerp >= 0.5f)
            {
                frogAnim.SetTrigger("FallJump");
            }
            
            
            if (slerp >= 1)
            {
                frogAnim.SetTrigger("Fall");
                slerp = 1;
                transform.position = EndPos;
                finish = true;
                if (TilesManager.current.actState == GameStates.frogPath)
                {
                    StartCoroutine(EndLerp());
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


    IEnumerator EndLerp()
    {
        if (TilesManager.current.actState != GameStates.frogPath)
            yield break; 

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
                    frogAnim.SetTrigger("Idle");
                    yield return ScriptsTools.GetWait(MidleStopTime);
                    StartCoroutine(TilesManager.current.EndGame());
                    yield break;
                }
                if(hit.transform.CompareTag("Death") )
                   frogAnim.SetTrigger("Lost");
                if(hit.transform.CompareTag("Water"))
                    frogAnim.SetTrigger("Water");
                yield return ScriptsTools.GetWait(MidleStopTime);
                StartCoroutine(TilesManager.current.EndGame());
                yield break;

            }


            if (tc.target == null)
            {
                if(hit.transform.CompareTag("Death") )
                    frogAnim.SetTrigger("Lost");
                if(hit.transform.CompareTag("Water"))
                    frogAnim.SetTrigger("Water");
                yield return ScriptsTools.GetWait(MidleStopTime);
                StartCoroutine(TilesManager.current.EndGame());
                yield break;
            }
   
            transform.LookAt(new Vector3(tc.target.position.x,transform.position.y,tc.target.position.z));
            SetEndPos();
            yield return ScriptsTools.GetWait(MidleStopTime);
            frogAnim.SetTrigger("StartJump");
            finish = false;
            yield break;
        }
        frogAnim.SetTrigger("Lost");
        yield return ScriptsTools.GetWait(MidleStopTime);
        StartCoroutine(TilesManager.current.EndGame());


    }


}
