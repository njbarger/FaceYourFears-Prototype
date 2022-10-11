using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public AudioClip projectileImpactSound;
    private GameObject hit;
    private bool collided;

    void Start()
    {
        rb.velocity = Camera.main.transform.forward * gameManager.instance.playerController.proSpeed;
        Destroy(gameObject, gameManager.instance.playerController.destroyTime);
    }
    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag != "Bullet" && col.gameObject.tag != "Player" && !collided && col.gameObject.tag != "Muzzle")
        {
            collided = true;
            hit = Instantiate(gameManager.instance.playerController.impactE, col.contacts[0].point, Quaternion.identity);

            if (col.collider.GetComponent<IDamageable>() != null)
            {
                col.collider.GetComponent<IDamageable>().takeDamage(gameManager.instance.playerController.playerDamage);
            }
        }
        if (collided == true)
        {
            Destroy(hit, 3);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(projectileImpactSound, transform.position);
    }
}
//[Header("----- Components -----")]
//[SerializeField] Rigidbody rb;

//[Header("----- Bullet Stats -----")]
//public int damage;
//public int speed;
//public int destroyTime;  //this is for clean up, so the frames arent falling. taking up too much memory.

//// Start is called before the first frame update
//void Start()
//{
//    rb.velocity = transform.forward * speed; // sends the bullet toward the transforms forward direction. 
//    Destroy(gameObject, destroyTime);
//}

//private void OnTriggerEnter(Collider other)
//{
//    if (other.GetComponent<IDamageable>() != null)
//    {
//        other.GetComponent<IDamageable>().takeDamage(damage);
//        Destroy(gameObject);
//    }

//}