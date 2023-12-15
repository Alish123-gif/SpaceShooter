using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{    // Start is called before the first frame update

    private Text text;
    private Image _liveDisplay;

    [SerializeField] private Sprite[]  _liveSprite;
    [SerializeField] private Transform _GameOver;
    [SerializeField] private Transform _Restart;
    private Text _gameOverText;
    void Start()
    {
        _GameOver = gameObject.transform.GetChild(2);
        _GameOver.gameObject.SetActive(false);
         _gameOverText = _GameOver.GetComponent<Text>();

        _Restart = gameObject.transform.GetChild(3);
        _Restart.gameObject.SetActive(false);

        Transform _Image = gameObject.transform.GetChild(1);
        _liveDisplay = _Image.GetComponent<Image>();

        Transform _Text = gameObject.transform.GetChild(0);
        text = _Text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        bool pressR = Input.GetKeyDown(KeyCode.R);
        if (pressR)
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void UpdateLiveDisplay(int _Lives){
        _liveDisplay.sprite = _liveSprite[_Lives];
        if(_Lives==0){
            _GameOver.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
            _Restart.gameObject.SetActive(true);
        }
    }
    public void UpdateScore(int Score){ 
        text.text = String.Format("Score: {0}",Score);
    }

    IEnumerator GameOverFlickerRoutine(){
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
