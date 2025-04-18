using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float totalHealth = 100.0f;
    [SerializeField] public float bulletDamage = 10.0f;
    public float initHealth;
    public bool isBoss;
    public bool isPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(totalHealth <= initHealth/2 && isBoss == true)
        {

        }
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Bullet" && isPlayer == true)
        {
            totalHealth -= bulletDamage;
            Debug.Log(totalHealth);
        }
    }

}
