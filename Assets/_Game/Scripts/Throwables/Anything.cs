using UnityEngine;
using Random = UnityEngine.Random;

public class Anything : PoolObject
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] anythings;
    public override void OnDeactivate()
    
    {
        gameObject.SetActive(false);
        rb.isKinematic = true;
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        rb.isKinematic = false;
        ShowRandomAnything();
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }

    public void Throw(Vector3 v)
    {
        rb.transform.LookAt(v);
        rb.AddForce(rb.transform.forward * v.magnitude / 2f + Vector3.up * 3f, ForceMode.Impulse);
    }

    private void ShowRandomAnything()
    {
        int rnd = Random.Range(0, anythings.Length);
        for (int i = 0; i < anythings.Length; i++)
        {
            anythings[i].SetActive(i == rnd);
        }
    }

    public float damaage;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyBase e = collision.collider.GetComponent<EnemyBase>();
            if (e)
            {
                e.HP -= damaage;
                OnDeactivate();
            }
        }
        if (collision.collider.CompareTag("Plane"))
        {
            OnDeactivate();
        }
    }
}
