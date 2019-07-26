using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitAll : MonoBehaviour
{
    public bool isHurt;
    private CheckHit HighCheck;
    private CheckHit MidCheck;
    private CheckHit LowCheck;

    void Awake()
    {
        isHurt = false;
        
        HighCheck = gameObject.transform.Find("HurtBoxHigh").gameObject.GetComponent<CheckHit>();
        MidCheck = gameObject.transform.Find("HurtBoxMid").gameObject.GetComponent<CheckHit>();
        LowCheck = gameObject.transform.Find("HurtBoxLow").gameObject.GetComponent<CheckHit>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isHurt = (HighCheck.isHurt || MidCheck.isHurt || LowCheck.isHurt);
    }

    public void SetFalse()
    {
        HighCheck.isHurt = false;
        MidCheck.isHurt = false;
        LowCheck.isHurt = false;
    }
}
