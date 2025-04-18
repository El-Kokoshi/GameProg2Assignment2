using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public float waitTime = 5.0f;
    [SerializeField] public float timerTime = 5.0f;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject gunUser;
    [SerializeField] public GameObject player;
    [SerializeField] public float bulletSpeed = 5.0f;
    [SerializeField] private bool isActive;
    public GameObject newBullet;
    public bool isSpawning;
    public bool isShotgun;
    public bool isRifle;
    
    
    public void Start() 
    {
        waitTime = timerTime;
    }
    
    //Checks if the spawner is working or not, if not starts the spawning process
    public void Update()
    {
        transform.position = gunUser.gameObject.transform.position;

        if(isSpawning == false && isActive == true)
        {
            timerTime -= Time.deltaTime;
            if(timerTime <= 0)
            {
                isSpawning = true;
                timerTime = waitTime;
                ObjectSpawner();
            }
        }
    }

    //Spawns the selected prefab and reinitiates the spawner
    public void ObjectSpawner()
    {
        if(isRifle == true)
        {
            newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * bulletSpeed;
            isSpawning = false;
        }
        else if(isShotgun == true)
        {
            newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * bulletSpeed;

            newBullet = Instantiate(bulletPrefab, transform.position + (player.transform.position - transform.position - Vector3.up).normalized, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * bulletSpeed;
            
            newBullet = Instantiate(bulletPrefab, transform.position + (player.transform.position - transform.position - Vector3.left).normalized, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * bulletSpeed;
            isSpawning = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = true;
        }

    }

    public void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }
}
