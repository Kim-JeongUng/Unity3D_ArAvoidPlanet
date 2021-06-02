using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public AudioClip explosionSound;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 80, Space.Self);

        if(GameObject.Find("Ship").GetComponent<AllSkill>().bomb)
        {
            StartCoroutine(Crash());
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ship")){
            StartCoroutine(Crash());
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ship"))    
        {
            SpaceShip.Hp -= 4;
        }
        if (other.gameObject.CompareTag("DestroyCollider"))
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Crash()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(explosionSound);
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}
