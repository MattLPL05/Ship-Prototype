using UnityEngine;
using System.Collections;

public class RemoveWindeffect : MonoBehaviour
{

    [SerializeField]
    private int DeleteTimer;

    [SerializeField]
    private Transform WindDir;

    //required for movement
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Yeeet()); //init
        rb = GetComponent<Rigidbody>();

        WindDir = GameObject.Find("WindHolder").transform; // not the best solution, but we will be in need of applying a local scene object into a script under a prefab..
    }

    private IEnumerator Yeeet()
    {
        yield return new WaitForSeconds(DeleteTimer);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0, WindDir.transform.rotation.y, 0, ForceMode.Impulse);
       transform.localRotation = Quaternion.Euler(0, WindDir.transform.rotation.y, 0);;
    }
}
