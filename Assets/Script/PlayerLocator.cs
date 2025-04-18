using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    [SerializeField] public float runSpeed = 5.5f;
    [SerializeField] public float patrolModifier = 2.0f;
    [SerializeField] public GameObject player;
    [SerializeField] public bool isAvoiding;
    public bool patrolDirection;
    [SerializeField] public bool isActive;
    [SerializeField] public float timerTime = 1.5f;
    public float timerSave;
    public Vector2 motionSave;
    

    void Start()
    {
        timerSave = timerTime;
    }

    
    void Update()
    {
        timerTime -= Time.deltaTime; // patrol timer
        if(timerTime <= 0.0f)
        {
            patrolDirection = !patrolDirection;
            timerTime = timerSave;
        }
        
        if(isAvoiding == false && isActive == true) // different combinations of these variables gives the enemy type
        {
            GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * runSpeed;
        }
        else if(isAvoiding == true && isActive == true)
        {
            GetComponent<Rigidbody2D>().linearVelocity = (transform.position - player.transform.position).normalized * runSpeed;
        }
        else if(isAvoiding == true && isActive == false)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        else if(isAvoiding == false && isActive == false && patrolDirection == true)
        {
            GetComponent<Rigidbody2D>().linearVelocity = motionSave/patrolModifier;
        }
        else if(isAvoiding == false && isActive == false && patrolDirection == false)
        {
            GetComponent<Rigidbody2D>().linearVelocity = (-1) * motionSave/patrolModifier;
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            isActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            motionSave = GetComponent<Rigidbody2D>().linearVelocity;
            isActive = false;
        }
    }

}
