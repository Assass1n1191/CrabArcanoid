using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScreen : MonoBehaviour 
{
    public static GameScreen Instance;

    public Image ui_PanelGameOver;

    public Text ui_Score;
    public List<HealthHeart> HealthBar;
    public Image Timer;
    private int _score;
    private int _currentHealth = 5;

    private float _gameDuration = 60f; //in seconds


    public bool GameIsStarted = false;
    public bool GameWasReseted = false;

	private void Awake () 
	{
        Instance = this;
	}

	private void Start () 
	{
        Time.timeScale = 1f;
	}
	
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetMouseButtonDown(0) && (!GameIsStarted || GameWasReseted))
        {
            Ball.Instance.InitMoveDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GameIsStarted = true;
            GameWasReseted = false;
        }

        if (!GameIsStarted) return;

        Timer.fillAmount -= (1f / _gameDuration) * Time.deltaTime;
        if (Timer.fillAmount == 0f)
            GameOver();
    }

    public void GameOver()
    {
        GameIsStarted = false;

        Time.timeScale = 0f;
        ui_PanelGameOver.gameObject.SetActive(true);
    }

    public void ResetToStart()
    {
        ChangeHealthAmount(-1);
        Ball.Instance.ResetPosition();
        Crab.Instance.ResetPosition();
        GameWasReseted = true;
    }

    public void ChangeHealthAmount(int changeValue)
    {
        _currentHealth += changeValue;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, 5);

        for(int i = 0; i < HealthBar.Count; i++)
        {
            HealthBar[i].ChangeSprite(i < _currentHealth);
        }

        if (_currentHealth == 0)
        {
            GameOver();
        }
    }

    public void ChangeScore(int changeAmount)
    {
        _score += changeAmount;
        ui_Score.text = "Score: " + _score.ToString();
    }

    public void ChangeTime(float changeAmount)
    {
        Timer.fillAmount += 1f / changeAmount;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
