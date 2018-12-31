using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SRP
{
    SCISSORS = 0, ROCK, PAPER, COUNT
};

public enum RESULT
{
    WIN=0, LOSE, DRAW, COUNT
};

public class Game : MonoBehaviour
{
    
    public float duration =2f;
    public Text countTime;
    public Text mySrp;
    public SRP sRP;
    private SRP nPC;
    private RESULT result;
    


    private void Start()
    {
        StartCoroutine(CountDown(duration));
        mySrp.text = "선택하세요!";
    }

    IEnumerator CountDown(float duration)
    {
        var beginTime = Time.time;
        var time = 0f;

        for(; ; )
        {
            time = duration - (Time.time - beginTime);
            if(time >= 0f)
            {
                countTime.text =  time.ToString("0.00") ;
            }
            else
            {
                countTime.text = "0.00f";
                break;
            }
            yield return null;
        }
        countTime.text = "승자는?";


        CompareNPC();
        ResultSrp();
    }

    void CompareNPC()
    {
        
        nPC = (SRP) Random.Range(0, (int)SRP.PAPER);

        if(nPC == sRP)
        {
            result = RESULT.DRAW;
        }
        else
        {
            switch (sRP)
            {
                case SRP.SCISSORS:
                    if(nPC == SRP.ROCK)
                    {
                        result = RESULT.LOSE;
                    }
                    else //if(nPC == SRP.PAPER)
                    {
                        result = RESULT.WIN;
                    }
                    break;
                case SRP.ROCK:
                    if (nPC == SRP.PAPER)
                    {
                        result = RESULT.LOSE;
                    }
                    else //if(nPC == SRP.SCISSORS)
                    {
                        result = RESULT.WIN;
                    }
                    break;
                case SRP.PAPER:
                    if (nPC == SRP.SCISSORS)
                    {
                        result = RESULT.LOSE;
                    }
                    else //if(nPC == SRP.ROCK)
                    {
                        result = RESULT.WIN;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    void ResultSrp()
    {
        countTime.text += "\n(나는 " + sRP + " || NPC는 " + nPC + ")";
        switch (result)
        {
            case RESULT.WIN:
                mySrp.text = "나!";
                break;
            case RESULT.LOSE:
                mySrp.text = "NPC!";
                break;
            case RESULT.DRAW:
                mySrp.text = "없어_비김!";
                break;
            default:
                mySrp.text = "없어_다시!";
                break;

        }
    }
}
