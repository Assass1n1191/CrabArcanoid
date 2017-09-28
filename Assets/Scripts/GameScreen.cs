using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScreen : MonoBehaviour 
{
    public static GameScreen Instance;

    public Image ui_PanelGameOver;

    public Text ui_Timer;
    public List<HealthHeart> HealthBar;
    public List<GameObject> Stars;
    public Image ui_Score;
    public Image ui_x2Image;

    private float _score;
    private int _currentHealth = 5;

    private int _gameDuration = 60; //in seconds
    private int _currentGameTime;

    public bool GameIsStarted = false;
    public bool GameWasReseted = false;

    private float _scoreMultiplier = 1f;
    private float _scoreMultiplierTimer;
    private bool _isScoreMultiplierOn;

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
            if (!GameWasReseted)
                StartCoroutine(Timer());

            Ball.Instance.InitMoveDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GameIsStarted = true;
            GameWasReseted = false;
        }

        if (!GameIsStarted) return;

        //Timer.fillAmount -= (1f / _gameDuration) * Time.deltaTime;
        //if (Timer.fillAmount == 0f)
        //    GameOver();

        MultiplierScoreTimer();
    }

    public void GameOver()
    {
        GameIsStarted = false;

        if(ui_Score.fillAmount > 0.33f)
            Stars[0].SetActive(true);
        if (ui_Score.fillAmount > 0.66f)
            Stars[1].SetActive(true);
        if (ui_Score.fillAmount > 0.99f)
            Stars[2].SetActive(true);

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

    public void ChangeScore(float changeAmount)
    {
        _score += changeAmount * _scoreMultiplier;
        ui_Score.fillAmount = _score / 10000f;

        if (_score >= 10000)
            GameOver();
    }

    public void ChangeTime(int changeAmount)
    {
        _currentGameTime += changeAmount;
        ui_Timer.text = "Time: " + _currentGameTime.ToString("00");
    }

    private IEnumerator Timer()
    {
        _currentGameTime = _gameDuration;

        while(_currentGameTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            _currentGameTime -= 1;
            ui_Timer.text = "Time: " + _currentGameTime.ToString("00");
        }

        GameOver();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void SetMultiplierBonus(float multiplier, float time)
    {
        _scoreMultiplier = multiplier;
        _scoreMultiplierTimer += time;
        _isScoreMultiplierOn = true;
        ui_x2Image.gameObject.SetActive(true);
    }

    private void MultiplierScoreTimer()
    {
        if (_isScoreMultiplierOn)
        {
            _scoreMultiplierTimer -= Time.deltaTime;
            if (_scoreMultiplierTimer <= 0)
            {
                _scoreMultiplier = 1f;
                _isScoreMultiplierOn = false;
                ui_x2Image.gameObject.SetActive(false);
            }
        }
    }
}
