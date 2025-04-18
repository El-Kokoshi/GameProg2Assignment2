using UnityEngine;

public class BossAttackS2 : MonoBehaviour
{
    [SerializeField] public float waitTime = 5.0f;
    [SerializeField] public float timerTime = 5.0f;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject gunUser;
    [SerializeField] public float bulletSpeed = 5.0f;
    public GameObject newBullet;
    public bool isSpawning;
    
    public void Start() 
    {
        waitTime = timerTime;
    }
    
    //Checks if the spawner is working or not, if not starts the spawning process
    public void Update()
    {
        transform.position = gunUser.gameObject.transform.position;

        if(isSpawning == false)
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

    public void ObjectSpawner()
    {
        newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().linearVelocity = Vector3.right * bulletSpeed;

        newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().linearVelocity = Vector3.left * bulletSpeed;

        newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().linearVelocity = Vector3.up * bulletSpeed;

        newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().linearVelocity = Vector3.down * bulletSpeed;

        isSpawning = false;
    }
}
