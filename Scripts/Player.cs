using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _life = 3;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _laseprefab;
    [SerializeField] private GameObject _TShot;
    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    private SpawnManager spawnManager;
    [SerializeField] private bool IsTShot;
    private float TShotDuration = 5.0f;
    [SerializeField] private GameObject _Shield;
    [SerializeField] private GameObject _UIManager;

    [SerializeField] private int _Score;
    void Start()
    {
        transform.position = new Vector3(0,0,0);

        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(spawnManager==null)
        {
            Debug.LogError("The Spawn Manager is Null.");
        }
        IsTShot=false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
           FireLaser();
        };
        
    }

    void CalculateMovement(){
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, verticalInput,0);
        transform.Translate(direction*_speed*Time.deltaTime);

        if(transform.position.x >= 11){
            transform.position = new Vector3(-11,transform.position.y,0);
        }
        else if(transform.position.x<=-11){
            transform.position = new Vector3(11,transform.position.y,0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), 0);
    }
    void FireLaser(){
            _canFire = Time.time + _fireRate;
            TShotDuration = TShotDuration+Time.time;
            if (IsTShot)
            {
                Instantiate(_TShot, transform.position + new Vector3(-0.06f, 0.2f,0),Quaternion.identity);
            }
            else{
                Instantiate(_laseprefab, transform.position + new Vector3(0,0.826f,0), Quaternion.identity);
            }
    }
    public void Damage(){
        _life--;
        
        if(_life == 2){
            int nb = Random.Range(0,2);
            this.gameObject.transform.GetChild(nb).gameObject.SetActive(true);
        }
        else if(_life == 1){
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        UIManager UIM = _UIManager.GetComponent<UIManager>();
        UIM.UpdateLiveDisplay(_life);

        if (_life<1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void PowerUpSet(int ID){
        switch(ID){
            case 0:
                IsTShot = true;
                StartCoroutine(PowerUpCoolDown(0));
                break;
            case 1:
                _speed*=2;
                StartCoroutine(PowerUpCoolDown(1));
                break;
            case 2:
                GameObject Shield = Instantiate(_Shield, this.transform.position+new Vector3(0,-0.1f,0), Quaternion.identity);
                Shield.transform.parent = this.transform;
                break;
        }
        
    }
    IEnumerator PowerUpCoolDown(int ID){
        switch(ID)
        {
            case 0:
                yield return new WaitForSeconds(5.0f);
                IsTShot = false;
                break;
            case 1:
                yield return new WaitForSeconds(7.0f);
                _speed/=2;
                break;
        }
        
    }
    public void addScore(int points){
        _Score += points;
        _UIManager.GetComponent<UIManager>().UpdateScore(_Score);

    }
    public int getScore(){
        return _Score;
    }
    
}