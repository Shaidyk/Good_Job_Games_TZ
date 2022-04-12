using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera cam;
    public GameObject prefab;

    public bool shoot = false;
    float scale;

    GameObject player;
    GameObject projectile;
    

    void Start()
    {
        player = this.gameObject;
        scale = player.transform.localScale.x;
    }

    private void FixedUpdate()
    {
        player.transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);  // Player Move

        if (projectile != null)
            projectile.transform.position = new Vector3(projectile.transform.position.x + 0.05f, projectile.transform.position.y, projectile.transform.position.z);
        
        if (projectile != null && shoot)
            projectile.transform.position = new Vector3(projectile.transform.position.x + 0.05f * 5, projectile.transform.position.y, projectile.transform.position.z);

        cam.transform.position = new Vector3(cam.transform.position.x + 0.05f, cam.transform.position.y, cam.transform.position.z);  // Camera Move

        if (player.transform.localScale.x <= scale / 100f * 40f)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene(0);
        }
    }


    private void OnMouseDown()
    {
        if (projectile == null)
        {
            projectile = Instantiate(
            prefab,
            new Vector3(
                transform.position.x + (transform.localScale.x / 2) + (prefab.transform.localScale.x / 2),
                transform.position.y,
                transform.position.z),
            transform.rotation);
        }
    }
    private void OnMouseDrag()
    {
        if (projectile != null && !shoot)
        {
            player.transform.localScale = new Vector3(transform.localScale.x - 0.001f, transform.localScale.y - 0.001f, transform.localScale.z - 0.001f);
            player.transform.position = new Vector3(transform.position.x, transform.position.y - (0.001f / 2), transform.position.z);

            projectile.transform.localScale = new Vector3(
                projectile.transform.localScale.x + 0.001f,
                projectile.transform.localScale.y + 0.001f,
                projectile.transform.localScale.z + 0.001f);
        }
        

    }

    private void OnMouseUp()
    {
        if (projectile != null)
        {
            projectile.transform.position = new Vector3(
            projectile.transform.position.x + 0.02f * 5,
            projectile.transform.position.y,
            projectile.transform.position.z);
            shoot = true;
        }
    }
}
