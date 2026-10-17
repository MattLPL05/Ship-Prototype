using UnityEngine;
using System.Collections;

public class WindDirectionManager : MonoBehaviour
{
    private int Randomiser;

    [SerializeField]
    private GameObject EffectSpawner;
    [SerializeField]
    private GameObject EffectTeplate;

    private bool RanOnce = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WindChange()); //init the wind change
        Randomiser = Random.Range(30, 300);
    }

    private IEnumerator WindChange()
    {
        yield return new WaitForSeconds(Randomiser);
        RunWindChange();
    }

    private void RunWindChange()
    {
        if(RanOnce == true) //to make sure we weon't spawn the wind in a direction we can't move at all upon starting
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 259) , 0);
            Randomiser = Random.Range(30, 300); //re-do the randomiser
            StartCoroutine(WindChange());
        }
        else
        {
            RanOnce = true;
        }
       
    }

    private void SpawnWindeffect() //removed due to
    {
        GameObject WindEffect = Instantiate(EffectTeplate);
        WindEffect.transform.localRotation = Quaternion.Euler(0, this.transform.rotation.y, 0);
        WindEffect.transform.LookAt(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
