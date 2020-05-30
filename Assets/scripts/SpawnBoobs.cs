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
    [SerializeField] BoxCollider2D barrierCollider; // for adjusting offsets, keep both the same size!!!!!!!!
    float leftSideX, rightSideX;

    [SerializeField] GameObject deathBar;
    public DeathBar deathBarMG;
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

        leftSideX = leftSide.position.x + barrierCollider.size.x / 2;

        rightSideX = rightSide.position.x - barrierCollider.size.x / 2;

        aimLine.SetActive(false);
        niceBoobPoof.SetActive(false);
        deathBarMG = deathBar.GetComponentInChildren<DeathBar>();
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
                    if (currentBoob.transform.position.x < rightSideX && (boobX - mouseX > 0.09f || boobX - mouseX < -0.09f))
                    {

                        currentBoob.transform.Translate(new Vector2(0.17f, 0));

                    }
                }
                //SLIDE TO THE LEFT
                else if (currentBoob.transform.position.x > leftSideX && (boobX - mouseX > 0.09f || boobX - mouseX < -0.09f))
                {
                    currentBoob.transform.Translate(new Vector2(-0.17f, 0));
                }

                if (currentBoob.transform.position.x > rightSideX)
                {
                    currentBoob.transform.position = new Vector2(rightSideX, currentBoob.transform.position.y);
                }
                if (currentBoob.transform.position.x < leftSideX)
                {
                    currentBoob.transform.position = new Vector2(leftSideX, currentBoob.transform.position.y);
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
        StartCoroutine("SpawnBoobOnSpawn", boobIndex);
    }
    IEnumerator SpawnBoobOnSpawn(int boobIndex)
    {
        yield return new WaitUntil(() => deathBarMG.touching == false);

        if (!BoobMG.instance.isGameOver)
        {

            //deathBar.SetActive(false);
            GameObject obj = Instantiate(boobs[boobIndex], transform.position, Quaternion.identity);
            currentBoob = obj;

            obj.transform.SetParent(transform);


            CircleCollider2D colliderTemp = currentBoob.GetComponent<CircleCollider2D>();
            float scaleTemp = currentBoob.transform.localScale.x;
            float tempOffset = (colliderTemp.radius * scaleTemp) + 0.15f; // lil extra on top because tits always crossing the boundaries??????
            leftSideX = leftSide.position.x + tempOffset;
            rightSideX = rightSide.position.x - tempOffset;
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

            if (next == 8)
            {  //if indexes change this wont work (wihtout fixing duh) (also index is the array index, not the index given on the OneBoob script)
                UpdateGoldenTiddies();
            }
        }
        else if (next == boobs.Length)
        {
            Debug.Log("godlike");
            BoobMG.instance.AddPoints(next * 2);
            niceBoobPoof.SetActive(true);


        }
    }

    IEnumerator BoobDropped()
    {
        yield return new WaitForSeconds(1f);
        //deathBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        SpawnBoob(Random.Range(0, 3));

    }

    void UpdateGoldenTiddies()
    {
        Debug.Log("noniin tissit");
        goldenTiddies++;
        goldenTiddiesText.text = goldenTiddies.ToString();
    }


}
