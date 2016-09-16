using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public Camera cam;
    public GameObject[] balls;
    public float timeLeft;
    public Text timerText;
    public Text scoreText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject startButton;

    public HatController hatController;

    private float maxWidth;
    private bool isPlaying;

    // Use this for initialization
    void Start()
    {        
        if (cam == null)
        {
            cam = Camera.main;
        }

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = balls[0].GetComponent<SpriteRenderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;

        timerText.text = "Time Left:\n" + timeLeft;
        isPlaying = false;
    }

    void FixedUpdate()
    {
        if (isPlaying)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft >= 0f)
            {
                timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft).ToString();
            }
            else
            {
                timerText.text = "Time Left:\n00";
            }
        }
    }

    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        hatController.ToggleControl();
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(2f);
        isPlaying = true;

        while (timeLeft > 0)
        {
            GameObject ball = balls[Random.Range(0, balls.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), 
                                    this.transform.position.y, 
                                    0);
            Instantiate(ball, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }

        yield return new WaitForSeconds(2f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(1f);
        restartButton.SetActive(true);
    }
}
