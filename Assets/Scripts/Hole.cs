using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Hole : MonoBehaviour
{
    public float initialHealth = 100f;
    private float timeBetweenDamage = 0.5f;

    private float health = 100f;

    public Sprite[] sizes;
    public float[] colliderRadius;
    public float[] timesBetweenDamage;
    public int pointsForPatch = 80;
    public GameObject VisualDamage;

    private int size;
    private float nextDamageTime;




    void Start()
    {
        transform.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        size = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().sprite = sizes[size];
        GetComponent<CircleCollider2D>().radius = colliderRadius[size];
        timeBetweenDamage = timesBetweenDamage[size];
        nextDamageTime = 0;
        health = initialHealth;
        GetComponent<AudioSource>().Play();
        transform.SetParent(GameObject.FindGameObjectWithTag("Airship").transform);
    }

    void Update()
    {
        ApplyDamageToAirship();
        CheckHealth();
    }

    void ApplyDamageToAirship()
    {
        if (isDead())
            return;
        if (Time.time < nextDamageTime)
            return;
        GameManager.instance.getAirship().ReduceAltitude(1);
        Instantiate(VisualDamage, transform.position, Quaternion.identity);
        nextDamageTime = Time.time + timeBetweenDamage;
    }

    public void ApplyingDamage(float dmg)
    {
        health -= dmg;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (!isDead())
            return;
        Patch p = ((GameObject)Instantiate(GameManager.instance.GetPatchToClone(), gameObject.transform.position, Quaternion.identity)).GetComponent<Patch>();
        GameManager.instance.GetPointController().GivePoints(pointsForPatch);

        p.size = size;
        p.transform.rotation = transform.rotation;
        GameObject.Destroy(gameObject);
    }

    public bool isDead()
    {
        return health < 0;
    }
}
