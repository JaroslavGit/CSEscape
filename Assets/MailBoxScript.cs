using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public AudioSource letterSound;
    private bool isReaded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isReaded && collision.gameObject.tag == "Player")
        { 
         letterSound.Play();
         isReaded= true;
        }
    }
}
