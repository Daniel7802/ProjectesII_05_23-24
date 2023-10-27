using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{

    //private PlayerController playerControllerScript;
    [SerializeField]
    GameObject player;

    //private Vector2 boomerangPosition;
    private bool coroutineAllowed;

    [SerializeField]
    private float speedModifier;

    private float tParam;
    

    // Start is called before the first frame update
    void Start()
    {
        tParam = 0f;
        speedModifier = 10f;
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(ThrowBoomerang());
       
    }
    private IEnumerator ThrowBoomerang()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector2 p0 = player.transform.position;
            Vector2 p2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 p2 = playerControllerScript.aimPoint;            
            Vector2 p1 = new Vector2((p0.x + p2.x) / 2, (p0.y + p2.y) / 2);
            
            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;

                transform.position = Mathf.Pow(1 - tParam, 2) * p0 + 2 * (1 - tParam) * tParam * p1 + Mathf.Pow(tParam, 2) * p2;

                //transform.position = objectToMovePosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;

                transform.position = Mathf.Pow(1 - tParam, 2) * p2 + 2 * (1 - tParam) * tParam * p1 + Mathf.Pow(tParam, 2) * p0;

                //transform.position = objectToMovePosition;
                yield return new WaitForEndOfFrame();
            }
            tParam = 0f;

        }       

    }
    
}

