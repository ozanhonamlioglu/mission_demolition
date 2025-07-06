using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [SerializeField] private int numClouds = 40;
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Vector3 cloudPosMin = new(-50, -5, 10);
    [SerializeField] private Vector3 cloudPosMax = new(150, 100, 10);
    [SerializeField] private float cloudScaleMin = 1;
    [SerializeField] private float cloudScaleMax = 3;
    [SerializeField] private float cloudSpeedMult = 0.5f; //

    private GameObject[] cloudInstances;
    
    private void Awake()
    {
        // Make an array large enough to hold all the Cloud
        cloudInstances = new GameObject[numClouds];
        var anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;
        for (var i = 0; i < numClouds; i++)
        {
            cloud = Instantiate(cloudPrefab, anchor.transform, true);
            var cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            var scaleU = Random.value;
            var scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU;
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloudInstances[i] = cloud;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var cloud in cloudInstances)
        {
            var scaleVal = cloud.transform.localScale.x;
            var cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            if (cPos.x <= cloudPosMin.x) cPos.x = cloudPosMax.x;
            cloud.transform.position = cPos;
        }
    }
}