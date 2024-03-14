using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    [Header("References")]
    public MusicGamplay musicGamplay;

    [Header("Scene Setings")]
    public GameObject fase1;
    public GameObject fase2;
    public GameObject fase3;

    public GameObject controles;
    public GameObject startButton;
    public GameObject hands;

    

    //privagate int currentFase = 1;
    public GameState currentState = GameState.Intro;


    [Header("Audio Setings")]
    public AudioSource[] music01;
    public bool startPlaying;


    public beatScroller bScroller;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextFinal;

    [Header("Musicas")]
    public SO_SongMidi[] tracks;

    [Header("Brindes")]
    public Image giveawayImage;
    public TextMeshProUGUI giveawayText;
    public SO_Giveaway[] giveawayGroup;
    public SO_Giveaway specialGiveaway;

    public int sorteado;

    public GameObject aviso;
    private GameObject sorteioObj;
    private CSVReader csvReader;

    [Header("Reset Timer")]
    public float resetTimeSeconds = 5;




    private void OnEnable()
    {
        musicGamplay.OnNoteHit += NoteInput;
        musicGamplay.OnMusicEnd += ActivateRewardFase;
        aviso.SetActive(false);
    }

    private void OnDisable()
    {
        musicGamplay.OnNoteHit -= NoteInput;
        musicGamplay.OnMusicEnd -= ActivateRewardFase;
    }

    void Start()
    {
        instance = this;
        // Começa com a primeira fase ativada e as outras desativadas
        ChangeGameState(GameState.Intro);
        scoreText.text = "0";
        scoreTextFinal.text = "0";

        sorteioObj = GameObject.Find("prizeSelector");
        sorteioObj.SetActive(true);
        csvReader = sorteioObj.GetComponent<CSVReader>();
        sorteado = csvReader.sorteado;

        controles.SetActive(false);
        startButton.SetActive(true);
        hands.SetActive(true);

    }

    void Update()
    {

        if (csvReader.esgotou)
        {

            aviso.SetActive(true);
        }
        // Exemplo de como avançar para a próxima fase (pode ser feito de acordo com o seu sistema de jogo)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case GameState.Intro:
                    ChangeGameState(GameState.Gameplay);
                    StartPlay();
                    break;
                case GameState.Gameplay:
                    ChangeGameState(GameState.Result);
                    break;
                case GameState.Result:
                    StopAllCoroutines();
                    ResetScene();
                    break;
            }
        }

        /*
        if(!startPlaying){
            if(Input.GetKeyDown(KeyCode.Space)){
                //Old_StartPlay()
                StartPlay();
            }
        }
        */


        // Special giveaway input
        if (Input.GetKeyDown(KeyCode.F12) && currentState == GameState.Intro)
        {
            PlayerPrefs.SetInt("GiveSpecialPrize", 1);
        }

        sorteado = csvReader.sorteado;
    }

    void Old_StartPlay()
    {
        startPlaying = true;
        bScroller.hasStarted = true;
        music01[2].Play();
    }

    public void StartPlay()
    {
        startPlaying = true;
        //bScroller.hasStarted = true;
        musicGamplay?.StartGame();
    }

    void PrepareMusic()
    {
        int musicId = PlayerPrefs.GetInt("MusicID");

        musicGamplay.SetTrack(tracks[musicId]);



        musicId = (musicId + 1) % tracks.Length;

        PlayerPrefs.SetInt("MusicID", musicId);
    }

    public void ChangeGameState(GameState newState)
    {
        // Desativa todas as fases
        fase1.SetActive(false);
        fase2.SetActive(false);
        fase3.SetActive(false);

        // Ativa a fase correspondente

        currentState = newState;

        switch (newState)
        {
            case GameState.Intro:
                hands.SetActive(true);
                fase1.SetActive(true);
                controles.SetActive(false);
                startButton.SetActive(true);
                PrepareMusic();
                break;
            case GameState.Gameplay:
                hands.SetActive(true);
                StartPlay();
                fase2.SetActive(true);
                controles.SetActive(true);
                startButton.SetActive(false);
                break;
            case GameState.Result:


                //exporta o csv
                hands.SetActive(false);
                SetupGiveaway();
                csvReader.SaveCSV();
                StartResetTimer();
                fase3.SetActive(true);
                controles.SetActive(false);
                break;
        }

    }

    void ActivateRewardFase()
    {
        ChangeGameState(GameState.Result);
    }

    void SetupGiveaway()
    {
        // Scriptable Objects stuff
        SO_Giveaway giveaway;

        // if is special prize
        if (PlayerPrefs.GetInt("GiveSpecialPrize") == 1)
        {
            giveaway = specialGiveaway;
            PlayerPrefs.SetInt("GiveSpecialPrize", 0);
        }
        else // Normal Prize
        {
            csvReader.DrawPrize();
            sorteado = csvReader.sorteado;

            giveaway = giveawayGroup[sorteado];
        }


        // Here update CSV or something like that

        giveawayImage.sprite = giveaway.sprite;
        giveawayText.text = giveaway.text;
    }

    void StartResetTimer()
    {
        StartCoroutine(ResetTimer());
        hands.SetActive(false);
    }

    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(resetTimeSeconds);

        // Restart Scene
        ResetScene();
    }

    void ResetScene()
    {
        sorteioObj.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void NoteInput(int noteId, NoteHitType hitType)
    {
        if (hitType == NoteHitType.Perfect)
        {
            NoteHit(true);
        }
        else if (hitType == NoteHitType.Hit)
        {
            NoteHit();
        }
        else
        {
            NoteMiss();
        }
    }


    public void NoteHit(bool perfect = false)
    {

        //Debug.Log("Note Hit" + currentScore.ToString());

        if (perfect){
            currentScore += scorePerNote * 2;
            
        }
        else{
            currentScore += scorePerNote;


        scoreText.text = currentScore.ToString();
        scoreTextFinal.text = currentScore.ToString();
        }
    }

    public void NoteMiss()
    {
        Debug.Log("Note Miss");
    }



}
