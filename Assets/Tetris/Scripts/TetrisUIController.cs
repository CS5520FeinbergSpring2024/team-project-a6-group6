using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TetrisUIController : MonoBehaviour
{
    public TMP_Text scoreText;
    public Board tetrisBoard;
    public Piece pieceComponent;

    // controls the UI components to be active or inactive when the buttons get hit
    public GameObject StartWordUI;
    public GameObject GameOverUI;
    public GameObject GameMenuUI;
    public GameObject TipsUI;

    // controls the music volume
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider SFXSlider;

    [HideInInspector]
    public bool isGameStarted;

    public void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVol", 0.75f); // Default value if not set
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVol", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVol", 0.75f);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        SFXSlider.onValueChanged.AddListener(SetEffectVolume);

        // Apply the initial volume settings
        SetMasterVolume(masterSlider.value);
        SetBgmVolume(bgmSlider.value);
        SetEffectVolume(SFXSlider.value);

        isGameStarted = false;
        //Debug.Log("UI controler: started");
    }

    public void ClearRowTrigger()
    {
        if (!tetrisBoard.IsGameOver)
        {
            scoreText.SetText((tetrisBoard.Score * 100).ToString());
            if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("Clear Line");
        }
    }

    public void GameOverTrigger()
    {
        EndGame();
        this.GameOverUI.SetActive(true);
    }


    public void ToggleMenu()
    {
        if (MultiSingleManager.Instance.isMulti) return;
        else
        {
            Time.timeScale = 1 - Time.timeScale;
            this.GameMenuUI.SetActive(!this.GameMenuUI.activeSelf);
        }
    }

    public void ToggleTips()
    {
        if (MultiSingleManager.Instance.isMulti) return;
        else
        {
            Time.timeScale = 1 - Time.timeScale;
            this.TipsUI.SetActive(!this.TipsUI.activeSelf);
        }
    }

    public void ReLoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene to restart the game
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayMusic("BGM");
        SceneManager.LoadScene("MainMenu");
    }


    public void StartGame()
    {
        if (!this.isGameStarted)
        {
            AudioManager.Instance.PlayMusic("BGM For Tetris");
            Time.timeScale = 1;
            this.StartWordUI.SetActive(false);
            this.isGameStarted = true;
            this.pieceComponent.OnGameStart();
            this.tetrisBoard.OnGameStart();
        }
    }


    public int EndGame()
    {
        Time.timeScale = 0;
        TMP_Text score = GameOverUI.transform.GetChild(0).GetComponent<TMP_Text>();
        score.SetText("Your score is\n" + (tetrisBoard.Score * 100).ToString());
        return tetrisBoard.Score*100;
    }

    // This function will control the master volume
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", LinearToDecibel(volume));
        PlayerPrefs.SetFloat("masterVol", volume);
    }

    public void SetBgmVolume(float volume)
    {
        audioMixer.SetFloat("bgmVolume", LinearToDecibel(volume));
        PlayerPrefs.SetFloat("bgmVol", volume);
    }

    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat("SoundEffect", LinearToDecibel(volume));
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    // Convert linear scale [0,1] to decibel scale [-80,0]
    private float LinearToDecibel(float linear)
    {
        return linear != 0 ? 20.0f * Mathf.Log10(linear) : -80.0f;
    }
}
