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
    WIN = 0, LOSE, DRAW, COUNT
};

public class Game : MonoBehaviour
{
    [HideInInspector] public int resultWinCount;
    [HideInInspector] public int resultLoseCount;
    [HideInInspector] public int resultDrawCount;
    public float duration = 5f;
    public Text countTime;
    public Text mySrp;
    public Text resultCountTxt;
    public Button startGame;
    public SRP sRP;
    private SRP nPC;
    private RESULT result;

    private void Awake()
    {
        LoadGameData();
    }

    private void Start()
    {
        mySrp.text = "선택하세요!";
    }

    private void LoadGameData()
    {
        resultWinCount = PlayerPrefs.GetInt("RESULT_WIN_COUNT", 0);
        resultLoseCount = PlayerPrefs.GetInt("RESULT_LOSE_COUNT", 0);
        resultDrawCount = PlayerPrefs.GetInt("RESULT_DRAW_COUNT", 0);
        ResetResultCount();
    }

    public void OnStartButton()
    {
        mySrp.text = "선택하세요!";
        StartCoroutine(GameProcess());
    }

    private IEnumerator GameProcess()
    {
        startGame.gameObject.SetActive(false);

        yield return StartCoroutine(CountDown(duration));

        CompareNPC();

        yield return StartCoroutine(ResultSrp());

        ResetResultCount();

        startGame.gameObject.SetActive(true);
    }

    private IEnumerator CountDown(float duration)
    {
        sRP = SRP.COUNT;
        var beginTime = Time.time;
        var time = 0f;

        for (; ; )
        {
            time = duration - (Time.time - beginTime);
            if (time >= 0f)
            {
                countTime.text = time.ToString("0.00");
            }
            else
            {
                countTime.text = "0.00f";
                break;
            }
            yield return null;
        }
    }

    private void CompareNPC()
    {
        nPC = (SRP)Random.Range(0, (int)SRP.PAPER);

        if (nPC == sRP)
        {
            result = RESULT.DRAW;
            return;
        }

        switch (sRP)
        {
            case SRP.SCISSORS:
                if (nPC == SRP.ROCK)
                {
                    result = RESULT.LOSE;
                }
                else
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

            case SRP.COUNT:
                result = RESULT.COUNT;
                break;

            default:
                break;
        }
    }

    private IEnumerator ResultSrp()
    {
        countTime.text = "승자는?";

        yield return new WaitForSeconds(1.0f);

        countTime.text += "\n(나는 " + sRP + " || NPC는 " + nPC + ")";
        switch (result)
        {
            case RESULT.WIN:
                mySrp.text = "나!";
                AddResultCount(result);
                break;

            case RESULT.LOSE:
                mySrp.text = "NPC!";
                AddResultCount(result);
                break;

            case RESULT.DRAW:
                mySrp.text = "없어_비김!";
                AddResultCount(result);
                break;

            case RESULT.COUNT:
                mySrp.text = "안냈어_다시!";
                break;

            default:
                mySrp.text = "없어_다시!";
                break;
        }
    }

    private void AddResultCount(RESULT result)
    {
        switch (result)
        {
            case RESULT.WIN:
                ++resultWinCount;
                PlayerPrefs.SetInt("RESULT_WIN_COUNT", resultWinCount);
                break;

            case RESULT.LOSE:
                ++resultLoseCount;
                PlayerPrefs.SetInt("RESULT_LOSE_COUNT", resultLoseCount);
                break;

            case RESULT.DRAW:
                ++resultDrawCount;
                PlayerPrefs.SetInt("RESULT_DRAW_COUNT", resultDrawCount);
                break;

            case RESULT.COUNT:
                break;

            default:
                break;
        }
    }

    private void ResetResultCount()
    {
        resultCountTxt.text = "| 총 승리횟수 : " + resultWinCount.ToString("000 |") +
                              "| 총 패배횟수 : " + resultLoseCount.ToString("000 |") +
                              "| 총 비긴횟수 : " + resultDrawCount.ToString("000 |");
    }
}