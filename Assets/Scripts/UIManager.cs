using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject gameOverText;
    public GameObject shotText;
    public GameObject wonText;
    public GameObject defeatedEnemiesText;
    public GameObject countdown;
    public GameObject hitscore;
    public GameObject deathscore;
    public static int enemiesKilled = 0;
    public static int shotsTaken = 0;
    [SerializeField] private float timedTextDuration = 2f;

    private void Start()
    {
        if (gameOverText == null)
        {
            gameOverText = SetText();
        }

        if (shotText == null)
        {
            shotText = SetText();
        }
        
        if (wonText == null)
        {
            wonText = SetText();
        }
        
        if (defeatedEnemiesText == null)
        {
            defeatedEnemiesText = SetText();
        }

        if (countdown == null)
        {
            countdown = SetText();
        }

        if (hitscore == null)
        {
            hitscore = SetText();
        }
        
        if (deathscore == null)
        {
            deathscore = SetText();
        }

        gameOverText.SetActive(false);
        shotText.SetActive(false);
        wonText.SetActive(false);
        defeatedEnemiesText.SetActive(false);
        hitscore.SetActive(false);
        deathscore.SetActive(false);

        StartCoroutine(TimeCountdown());

        if (GameObject.FindWithTag("Player").GetComponent<PlayerEventManager>().roomNumber == 1)
        {
            PlayerEventManager.onShot.AddListener(delegate { DisplayText(gameOverText); });
            PlayerEventManager.onShot.AddListener(delegate { DisplayTimedText(shotText); });
        }

        PlayerEventManager.onDefeatedEnemies.AddListener(delegate {DisplayText(wonText);});
        PlayerEventManager.onDefeatedEnemies.AddListener(delegate {DisplayTimedText(defeatedEnemiesText);});
    }

    private void Update()
    {
        hitscore.GetComponent<Text>().text = "You killed " + enemiesKilled + " enemy";
        deathscore.GetComponent<Text>().text = "You took " + shotsTaken + " shots";
    }


    private void DisplayText(GameObject displayText)
    {
        displayText.SetActive(true);
    }

    private void DisplayTimedText(GameObject timedText)
    {
        StartCoroutine(TimerTextCoroutine(timedText));
    }

    private IEnumerator TimerTextCoroutine(GameObject timedText)
    {
        timedText.SetActive(true);
        yield return new WaitForSeconds(timedTextDuration);
        timedText.SetActive(false);
    }

    private GameObject SetText() 
    {
        GameObject newGameObject;
        newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.AddComponent<Text>();
        return newGameObject;
    }

    private IEnumerator TimeCountdown()
    {
        Debug.Log("Countdown Coroutine");
        int count = 0;
        float truceTime = TimeFreeze.loadupTruce;
        while (count < 2)
        {
            float time = truceTime - count;
            countdown.GetComponent<Text>().text = "Game starts in: " + time + " seconds";
            count += 1;
            yield return new WaitForSecondsRealtime(1);
        }
        countdown.SetActive(false);
        hitscore.SetActive(true);
        deathscore.SetActive(true);
    }
}
