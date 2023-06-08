using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DriftManager : MonoBehaviour
{
    public Rigidbody playerRB;
    public Text totalScoreText;
    public Text currentScoreText;
    public Text factorText;
    public Text driftAngleText;


    private float speed = 0;
    private float driftAngle = 0;
    private float driftFactor = 1;
    private float currentScore;
    private float totalScore;

    private bool isDrifting = false;

    public float minimumSpeed = 5;
    public float minimumAngle = 10;
    public float driftingDelay = 0.2f;
    public GameObject driftingObject;
    public Color normalDriftColor;
    public Color nearStopColor;
    public Color driftEndedColor;

    private IEnumerator stopDriftingCoroutine=null;
    // Start is called before the first frame update
    void Start()
    {
        driftingObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ManageDrift();
        ManageUI();
    }

    void ManageDrift()
    {
        speed = playerRB.velocity.magnitude;
        driftAngle = Vector3.Angle(playerRB.transform.forward, (playerRB.velocity + playerRB.transform.forward).normalized);
        if (driftAngle > 120)
        {
            driftAngle = 0;
        }
        if(driftAngle>=minimumAngle && speed > minimumSpeed)
        {
            if (!isDrifting || stopDriftingCoroutine != null)
            {
                StartDrift();
            }
        }
        else
        {
            if (isDrifting && stopDriftingCoroutine == null)
            {
                StopDrift();
            }
        }
        if (isDrifting)
        {
            currentScore += Time.deltaTime * driftAngle * driftFactor;
            driftFactor += Time.deltaTime;
            driftingObject.SetActive(true);
        }
    }

    async void StartDrift()
    {
        if (!isDrifting)
        {
            await Task.Delay(Mathf.RoundToInt(1000 * driftingDelay));
            driftFactor = 0.5f;
        }
        if (stopDriftingCoroutine != null)
        {
            StopCoroutine(stopDriftingCoroutine);
            stopDriftingCoroutine = null;
        }
        currentScoreText.color = normalDriftColor;
        isDrifting = true;
    }

    void StopDrift()
    {
        stopDriftingCoroutine = StopingDrift();
        StartCoroutine(stopDriftingCoroutine);
    }

    private IEnumerator StopingDrift()
    {
        yield return new WaitForSeconds(0.5f);
        currentScoreText.color = nearStopColor;
        yield return new WaitForSeconds(driftingDelay * 4f);
        totalScore += currentScore;
        isDrifting = false;
        currentScoreText.color = driftEndedColor;
        yield return new WaitForSeconds(0.9f);
        currentScore = 0;
        driftingObject.SetActive(false);
    }

    void ManageUI()
    {
        totalScoreText.text = "Total score: " + (totalScore).ToString("###,###,000");
        factorText.text = driftFactor.ToString("###,###,##0") + "X";
        currentScoreText.text = currentScore.ToString("###,###,000");
        driftAngleText.text = driftAngle.ToString("###,##0") + "°";
    }
}
