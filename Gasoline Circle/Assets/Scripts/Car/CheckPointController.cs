using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{

    [SerializeField]
    private CarControllMode target;

    private bool[] resultCheck;

    public CarControllMode Target { get => target; set => target = value; }

    private void Awake()
    {
        resultCheck = new bool[3];
    }

    public void SetResultCheck(CheckPointType type) 
    {
        switch (type) 
        {
            case CheckPointType.FirstZone:             
                resultCheck[0] = true;        
                break;
            case CheckPointType.SecondZone:
                resultCheck[1] = true;
                break;
            case CheckPointType.ThirdZone:
                resultCheck[2] = true;
                break;
        }
    }

    public void FinishDetected() 
    {
        if (CheckResults())
            SetScorePlayer();
        else
            ResetResults();
    }

    private bool CheckResults() 
    {
        foreach (bool r in resultCheck) 
        {
            if (r == false)
                return false;
        }
        return true;
    }

    private void ResetResults() 
    {
        for (int i = 0; i < resultCheck.Length; i++) 
        {
            resultCheck[i] = false;
        }
    }


    private void SetScorePlayer() 
    {
        GameController.Instance.IncreaseThePlayerPoint(Target);
    }
}
