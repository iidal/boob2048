using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBoob : MonoBehaviour
{
    public int index;
    string boobTag;
    public bool boobMoving = false;
    public Rigidbody2D rigid;
    [SerializeField]HingeJoint2D tasseli;

    void Awake()
    {
        
        boobTag = "boob" + index.ToString();
        tag = boobTag;
        if(index == 8){
            
            tasseli =  gameObject.transform.Find("tasel").GetComponentInChildren<HingeJoint2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid != null)
        {  // is the boobie moving
            if (rigid.velocity.magnitude == 0)
            {
                boobMoving = true;
            }
            else
            {
                boobMoving = false;
            }
        }
    }

    public void BoobDropped()
    {
        
        rigid = GetComponent<Rigidbody2D>();

        if (index == 8)
        {
            tasseli.connectedBody = rigid;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == boobTag)
        {


            OneBoob script = other.GetComponent<OneBoob>();
            if (script != null && script.GetInstanceID() > GetInstanceID())
            {
                SpawnBoobs.instance.SpawnOnCollision(index, transform.position); //not index + 1 because arrays u know
                AudioManager.instance.PlayMatchSound();
            }


            Destroy(this.gameObject);
        }
        else if (other.tag == "boob" || other.tag == "floor")
        {
            AudioManager.instance.PlayBoobSoundEffect();
        }
    }
}
