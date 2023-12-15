using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    [SerializeField] private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime);

        if (transform.position.y<-5.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.PowerUpSet(0);
                        break;
                    case 1:
                        player.PowerUpSet(1);
                        break;   
                    case 2:
                        player.PowerUpSet(2);
                        break;

                }  
            }
            else{
                Debug.Log("Couldn't get the player object");
            }
            Destroy(this.gameObject);
        }
    }
    
}
