using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollMap : MonoBehaviour
{
    [SerializeField] Transform infiniteMap;

    bool isMoving = false;
    int heroLayer = 3;
    Vector3 mapDelta = new Vector3(113.7f, 0, 0);
    WaitForSeconds oneSec = new WaitForSeconds(1);  
    
    public void InfiniteMove()
    {
        infiniteMap.transform.position += mapDelta;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == heroLayer && isMoving ==false)
        {
            isMoving = true;
            StartCoroutine(CCoolDown());
            InfiniteMove();
        }
    }

    IEnumerator CCoolDown()
    {
        yield return oneSec;
        isMoving = false;
    }
}