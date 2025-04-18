using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == -0)
        {
            Debug.Log("All enemies defeated");
            SceneManager.LoadScene(sceneName);
        }
       
            
       




        // Player rotation toward mouse cursor
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 rotatedirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = rotatedirection;



        //Player movement
         Vector3 direction = Vector3.zero;
         if (Input.GetKey(KeyCode.W))
         {
             direction += Vector3.up;
         }
         if (Input.GetKey(KeyCode.S))
         {
             direction += Vector3.down;
         }
         if (Input.GetKey(KeyCode.A))
         {
             direction += Vector3.left;
         }
         if (Input.GetKey(KeyCode.D))
         {
             direction += Vector3.right;
         }
         transform.position += direction * 5.0f * Time.deltaTime;
    }
}
