using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField] public float timerTime = 1.5f;
    [SerializeField] public float dashSpeed = 3.5f;
    [SerializeField] public GameObject player;
    [SerializeField] public Health health;
    public float timerSave;
    public bool bossActive;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerSave = timerTime;
    }

    // Update is called once per frame
    void Update()
    {
        timerTime -= Time.deltaTime; // timer for boss dashing (stage1 movement)
        if(timerTime <= 0.0f && bossActive == true)
        {
            if(health.isStage2 == true) // stage 2 movement
            {
                transform.position += (player.transform.position - transform.position)/2;
            }

            GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * dashSpeed;
            timerTime = timerSave;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            bossActive = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            bossActive = false;
            GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        }   
    }
}
