using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
    public float initialHealth = 100f;
    private float altitudeLossPerSecond = 1.5f;

    private float health = 100f;

    public Sprite[] sizes;
    public float[] colliderRadius;
    public float[] altitudeLossPerSeconds;
    public int pointsForPatch = 80;

    private int size;

	void Start () {
        transform.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0,360));
        size = Random.Range(0,3);
        GetComponent<SpriteRenderer>().sprite = sizes[size];
        GetComponent<CircleCollider2D>().radius = colliderRadius[size];
        altitudeLossPerSecond = altitudeLossPerSeconds[size];
        health = initialHealth;
    }

    void Update () {
        ApplyDamage();
        CheckHealth();
    }

    void ApplyDamage()
    {
        if (health <= 0)
            return;
        GameManager.instance.getAirship().ReduceAltitude(altitudeLossPerSecond * Time.deltaTime);
    }

    public void ApplyingDamge(float dmg)
    {
        health -= dmg;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (health >= 0)
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
