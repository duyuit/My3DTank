using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public Text m_MessageText;              
    public GameObject m_Player;
    public GameObject m_AITankPrefab;
    public GameObject[] spawnDestination;

    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;
    private List<GameObject> listAITank = new List<GameObject>();

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAITanks();


        StartCoroutine(GameLoop());
    }
   

    private void SpawnAITanks()
    {
        var tankAI1 = Instantiate(m_AITankPrefab, spawnDestination[0].transform.position, Quaternion.identity);
        listAITank.Add(tankAI1);
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());
        SceneManager.LoadScene(0);
    }


    private IEnumerator RoundStarting()
    {
        foreach(var tankAI in listAITank)
        {
            tankAI.GetComponent<AIShooting>().enabled = false;
            tankAI.GetComponent<AITank>().enabled = false;
        }
        m_Player.GetComponent<TankHealth>().enabled = false;
        m_Player.GetComponent<TankMovement>().enabled = false;
        m_Player.GetComponent<TankShooting>().enabled = false;

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        foreach (var tankAI in listAITank)
        {
            tankAI.GetComponent<AIShooting>().enabled = true;
            tankAI.GetComponent<AITank>().enabled = true;
        }
        m_Player.GetComponent<TankHealth>().enabled = true;
        m_Player.GetComponent<TankMovement>().enabled = true;
        m_Player.GetComponent<TankShooting>().enabled = true;
        m_MessageText.enabled = false;
        while (!OneTankLeft())
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        m_MessageText.enabled = true;
        if (m_Player.activeSelf)
            m_MessageText.text = "Win";
        else m_MessageText.text = "Lose";
        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int count = 0;
        if (m_Player.activeSelf)
            count++;
        foreach(var tankAI in listAITank)
        {
            if (tankAI.activeSelf)
                count++;
        }
        return count <= 1;
    }

}