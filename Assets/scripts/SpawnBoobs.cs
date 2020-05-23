using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnBoobs : MonoBehaviour
{
    public static SpawnBoobs instance;
    [SerializeField] GameObject[] boobs;    //prefabs
    [SerializeField] GameObject boobParent; //parents all boobs
    //public List<OneBoob> allBoobs = new List<OneBoob>();
    [SerializeField] Transform leftSide, rightSide; //boundaries
    float leftSideX, rightSideX;

    [SerializeField] GameObject deathBar;
    [SerializeField] GameObject aimLine;
    public GameObject currentBoob;  //most recent boobie

    [SerializeField] GameObject boobPoof;

    [SerializeField] GameObject niceBoobPoof;
    
    int goldenTiddies = 0;
    [SerializeField] TextMeshProUGUI goldenTiddiesText;

    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        leftSideX = leftSide.position.x + 0.7f;
        rightSideX = rightSide.position.x - 0.7f;

        aimLine.SetActive(false);
        niceBoobPoof.SetActive(false);
        //SpawnBoob(0);
    }
    void Update()
    {
        //MOVE
        if (Input.GetMouseButton(0))
        {
            if (currentBoob != null)
            {

                aimLine.SetActive(true);

                float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                float boobX = currentBoob.transform.position.x;

                //STEP TO THE RIGHT
                if (boobX < mouseX)
                {
                    if (currentBoob.transform.position.x < rightSideX && (boobX - mouseX > 0.08f || boobX - mouseX < -0.08f))
                    {

                        currentBoob.transform.Translate(new Vector2(0.15f, 0));
                        
                    }
                }
                //SLIDE TO THE LEFT
                else if (currentBoob.transform.position.x > leftSideX && (boobX - mouseX > 0.08f || boobX - mouseX < -0.08f))
                {
                    currentBoob.transform.Translate(new Vector2(-0.15f, 0));
                }

                aimLine.transform.position = new Vector2(currentBoob.transform.position.x, aimLine.transform.position.y);


            }
        }

        //DROP
        if (Input.GetMouseButtonUp(0) && BoobMG.instance.isGameOver == false)
        {
            if (currentBoob != null)
            {
                currentBoob.AddComponent<Rigidbody2D>();
                currentBoob.transform.SetParent(boobParent.transform);
                currentBoob.GetComponent<OneBoob>().BoobDropped();
                currentBoob = null;
                aimLine.SetActive(false);
                StartCoroutine("BoobDropped");  //jos tä homma ei muutu ni vaihda invokeksi
            }
        }


    }

    public void SpawnBoob(int boobIndex)
    {
        if (!BoobMG.instance.isGameOver)
        {
            //deathBar.SetActive(false);
            GameObject obj = Instantiate(boobs[boobIndex], transform.position, Quaternion.identity);
            currentBoob = obj;

            obj.transform.SetParent(transform);
           // allBoobs.Add(obj.GetComponent<OneBoob>());
        }

    }
    public void SpawnOnCollision(int next, Vector2 newPos)
    {

        Instantiate(boobPoof, newPos, boobPoof.transform.rotation);
        if (next < boobs.Length)
        {
            
            GameObject obj = Instantiate(boobs[next], newPos, Quaternion.identity);
            obj.AddComponent<Rigidbody2D>();
            obj.transform.SetParent(boobParent.transform);
            obj.GetComponent<OneBoob>().BoobDropped();
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
            
            
            BoobMG.instance.AddPoints(next);

            if(next == 8){  //if indexes change this wont work (wihtout fixing duh) (also index is the array index, not the index given on the OneBoob script)
                UpdateGoldenTiddies();
            }
        }
        else
        {
            Debug.Log("godlike");
            BoobMG.instance.AddPoints(next*2);
            niceBoobPoof.SetActive(true);
          
            
        }
    }

    IEnumerator BoobDropped()
    {
        yield return new WaitForSeconds(1f);
        //deathBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        SpawnBoob(Random.Range(0,3));
        
    }

    void UpdateGoldenTiddies(){
        Debug.Log("noniin tissit");
        goldenTiddies++;
        goldenTiddiesText.text = goldenTiddies.ToString();
    }


}
