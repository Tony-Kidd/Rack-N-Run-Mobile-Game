using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightchange;

    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    public float randomSpikeThreshold;
    public ObjectPooler spikePool;


    // Start is called before the first frame update
    void Start()
    {

        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;

            minHeight = transform.position.y;
            maxHeight = maxHeightPoint.position.y;

            theCoinGenerator = FindObjectOfType<CoinGenerator>();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightchange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightchange > maxHeight)
            {
                heightchange = maxHeight;
            }
            else if (heightchange < minHeight)
            {
                heightchange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightchange, transform.position.z);


            //Instantiate (/* thePlatform */ thePlatforms[platformSelector], transform.position, transform.rotation);

           GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {

                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                GameObject newSpike = spikePool.GetPooledObject();

                float spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2f + 2F, platformWidths[platformSelector] / 2f - 1f);

                Vector3 spikePosition = new Vector3(spikeXPosition, 1f, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }

    }
}
