using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 lastDir;
    private void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    public void RotateBall()
    {
        this.transform.Rotate(new Vector3(30 * Time.deltaTime, 0, 0));
    }
    void Update()
    {
        RotateBall();

        Cache.Instance.CollectObject(gameObject, 20f);

        
    }
    private void LateUpdate()
    {
        lastDir = rb.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ZhuoMian")
        {
            Vector3 refexangle = Vector3.Reflect(lastDir, collision.contacts[0].normal);
            rb.velocity = refexangle.normalized * lastDir.magnitude;
        }
    }
}
