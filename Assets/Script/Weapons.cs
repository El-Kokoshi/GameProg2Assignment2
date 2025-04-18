using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
    RIFLE,
    SHOTGUN,
    GRENADELAUNCHER,
    COUNT
}

public abstract class Weapon
{
    // "How do we want *ALL* bullets to behave?" -- Each have a unique direction & speed
    public abstract void Shoot(Vector3 direction, float speed);

    // "How do we want *ALL* weapons to behave?" -- Each weapon needs a prefab, and a shooter
    public GameObject weaponPrefab;
    public GameObject shooter;

    public int ammoCount;    // How much amo is currently in our clip
    public int ammoMax;      // How much amo does our clip hold

    public float reloadCurrent; // How far into our reload cooldown
    public float reloadTotal;   // How long it takes to reload

    public float shootCurrent;  // How far into our shoot cooldown
    public float shootTotal;

   

    
}   


public class Rifle : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {
        GameObject bullet = GameObject.Instantiate(weaponPrefab);
        bullet.transform.position = shooter.transform.position + direction * 0.75f;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        bullet.GetComponent<SpriteRenderer>().color = Color.red;
        GameObject.Destroy(bullet, 1.0f);
    }
}
//Shotgun class that derives from Weapon and implements the Shoot method
public class Shotgun : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {
        for (int i = -1; i <= 1; i++)
        {
            Vector3 spreadDirection = Quaternion.Euler(0, 0, i * 7) * direction;
            GameObject bullet = GameObject.Instantiate(weaponPrefab);
            bullet.transform.position = shooter.transform.position + spreadDirection * 0.75f;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = spreadDirection * speed;
            bullet.GetComponent<SpriteRenderer>().color = Color.green;
            GameObject.Destroy(bullet, 1.0f);
        }
    }
}
//GrenadeLauncher class that derives from Weapon and implements the Shoot method
public class GrenadeLauncher : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {

        GameObject grenade = GameObject.Instantiate(weaponPrefab);
        grenade.transform.position = shooter.transform.position + direction * 0.75f;
        grenade.GetComponent<Rigidbody2D>().linearVelocity = direction * speed * 0.75f;
        grenade.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        GameObject.Destroy(grenade, 1.20f);
    }
}


public class Weapons : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject grenadePrefab;

    private float bulletSpeed = 20.0f;
    //private float moveSpeed = 5.0f;

    WeaponType weaponType = WeaponType.RIFLE;
    Weapon rifle = new Rifle();
    Weapon shotgun = new Shotgun();
    Weapon granadeLauncher = new GrenadeLauncher();
    Weapon weapon = null;

    


    void Start()
    {

        rifle.weaponPrefab = bulletPrefab;
        rifle.shooter = gameObject;
        rifle.ammoMax = 25;
        rifle.ammoCount = rifle.ammoMax;
        rifle.shootCurrent = 0.0f;
        rifle.shootTotal = 0.25f;
        rifle.reloadTotal = 1.0f;
       

     

        shotgun.weaponPrefab = bulletPrefab; 
        shotgun.shooter = gameObject;
        shotgun.ammoMax = 15;
        shotgun.ammoCount = shotgun.ammoMax;
        shotgun.shootCurrent = 0.0f;
        shotgun.shootTotal = 1.0f;
        shotgun.reloadTotal = 2.0f;
        


        granadeLauncher.weaponPrefab = bulletPrefab; 
        granadeLauncher.shooter = gameObject;
        granadeLauncher.ammoMax = 5;
        granadeLauncher.ammoCount = granadeLauncher.ammoMax;
        granadeLauncher.shootCurrent = 0.0f;
        granadeLauncher.shootTotal = 2.0f;
        granadeLauncher.reloadTotal = 4.0f;

        

        weapon = rifle;

       
    }

    void Update()
    {
       float dt = Time.deltaTime;
       
        // Aiming with mouse cursor
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse.z = 0.0f;
        Vector3 mouseDirection = (mouse - transform.position).normalized;
        Debug.DrawLine(transform.position, transform.position + mouseDirection * 5.0f);

        // Shoot weapon with mouse
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            weapon.shootCurrent += dt;
            if (weapon.shootCurrent >= weapon.shootTotal && weapon.ammoCount > 0 /*Add a check to make sure there are bullets in your clip*/)
            {
                weapon.shootCurrent = 0.0f;
                weapon.Shoot(mouseDirection, bulletSpeed);

                weapon.ammoCount--;

                
            }

            
            if (weapon.ammoCount == 0) 
            {
                weapon.reloadCurrent += dt;
                if (weapon.reloadCurrent >= weapon.reloadTotal)
                {
                    weapon.reloadCurrent = 0.0f;
                    weapon.ammoCount = weapon.ammoMax; 
                }
            }

        }

        // Cycle weapon with left-shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int weaponNumber = (int)++weaponType;
            weaponNumber %= (int)WeaponType.COUNT;
            weaponType = (WeaponType)weaponNumber;
            Debug.Log("Selected weapon: " + weaponType);
            switch (weaponType)
            {
                case WeaponType.RIFLE:
                    weapon = rifle;
                    break;

                case WeaponType.SHOTGUN:
                    // TODO: add shotgun
                    weapon = shotgun;
                    break;

                case WeaponType.GRENADELAUNCHER:
                    // TODO: add grenade
                    weapon = granadeLauncher;
                    break;
               


            }
        }

        
    }

    
}
