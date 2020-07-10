using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject UI;
    public GameObject explosion;
    public float radius=3f;
    public float power=500f;

    public void Explode()
    {
        UI.SetActive(false);
        Addforce();
        Instantiate(explosion, this.transform);
        StartCoroutine("DestroyObject");

    }

    public void Addforce()
    {
        Collider[] other = Physics.OverlapSphere(this.transform.position, radius);
        foreach(Collider hit in other)
        {
            
            Rigidbody rigid = hit.GetComponent<Rigidbody>();
            if (rigid != null&&hit.CompareTag("Dynamics"))
            {
                rigid.AddExplosionForce(power, this.transform.position, radius);
            }
        }
    
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
