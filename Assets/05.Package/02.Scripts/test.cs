using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TruckSprite의 Collider2D와 몬스터가 충돌하면 카운트 증가
       
        Debug.Log("Collider count increased: ");


     
    }

}
