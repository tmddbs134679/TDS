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
        // TruckSprite�� Collider2D�� ���Ͱ� �浹�ϸ� ī��Ʈ ����
       
        Debug.Log("Collider count increased: ");


     
    }

}
