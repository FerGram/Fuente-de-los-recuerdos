using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _destroyTime = 5f;
    [SerializeField] float _speed = 5f;

    private bool _flying = false;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player" && !_flying){

            _flying = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 2f) * _speed, ForceMode2D.Impulse);
            GetComponent<Animator>().enabled = true;
            Destroy(gameObject, _destroyTime);
        }
    }  
}
