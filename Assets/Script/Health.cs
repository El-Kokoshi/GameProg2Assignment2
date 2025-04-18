using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public float totalHealth = 100.0f;
    [SerializeField] public float bulletDamage = 10.0f;
    [SerializeField] public GameObject riflebox;
    [SerializeField] public SpriteRenderer spriteR;
    [SerializeField] public Sprite sprite;
    public float initHealth;
    public bool isBoss;
    public bool isPlayer;
    public bool isEnemy;

    public Image playerHealthBar;
    public bool isStage2;

    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        riflebox.SetActive(false);
        initHealth = totalHealth;
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            playerHealthBar.fillAmount = totalHealth / 100;

            if (totalHealth <= 0)
            {
                SceneManager.LoadScene(sceneName);
            }


        }


        if(totalHealth <= initHealth/2 && isBoss == true)
        {
            isStage2 = true;
            spriteR.sprite = sprite;
            riflebox.SetActive(true);
            Debug.Log(isStage2);
        }

        if(totalHealth <= 0 && isPlayer == false)
        {
            Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "PlayerBullet" && isEnemy == true)
        {
            totalHealth -= bulletDamage;
            Debug.Log(totalHealth);
        }
        else if(other.gameObject.tag == "PlayerBullet" && isBoss == true)
        {
            totalHealth -= bulletDamage;
            Debug.Log(totalHealth);
        }
    }

}
