using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    public GameObject emptyObject;
    private float startRadius = 0.5f;

    Player player;
    GameObject barrier;
    CapsuleCollider barrierCollider;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        barrier = this.gameObject;
        barrierCollider = barrier.GetComponent<CapsuleCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Poisoned")
        {
            barrier.tag = "Poisoned";

            MeshRenderer mr = barrier.GetComponent<MeshRenderer>();
            mr.materials[0].color = new Color(255, 133, 0, 255);

            StartCoroutine(BarrierPoisonRutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene(0);
        }

        if (barrier.tag == "Poisoned")
            return;

        if (other.tag == "Projectile")
        {
            barrier.tag = "Poisoned";

            if (other.transform.localScale.x > 0.3f)
            {
                barrierCollider.radius = startRadius + other.transform.localScale.x;
            }


            MeshRenderer mr = barrier.GetComponent<MeshRenderer>();
            mr.materials[0].color = new Color(255, 133, 0, 255);
            player.shoot = false;

            StartCoroutine(ProjectilePoisonRutine(other));

        }
        
        
    }

    

    IEnumerator ProjectilePoisonRutine(Collider other)
    {
        Destroy(other.gameObject);
        
        yield return new WaitForSeconds(0.1f);
        barrierCollider.radius = startRadius;

        yield return new WaitForSeconds(1);
        Destroy(barrier);
    }

    IEnumerator BarrierPoisonRutine()
    {
        yield return new WaitForSeconds(1);
        Destroy(barrier);
    }
}
