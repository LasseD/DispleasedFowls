using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
    public float initialHealth = 100f;
    public float altitudeLossPerSecond = 1.5f;
    public Patch patchToClone;

    private float health;
    private Airship airship;

	void Start () {
        if (patchToClone == null)
            throw new MissingComponentException("Patch to clone must be a patch at position 0,0 - use prefab.");
        health = initialHealth;
        airship = GameObject.FindGameObjectWithTag("Airship").GetComponent<Airship>();
    }

    void Update () {
        ApplyDamage();
        CheckHealth();
    }

    void ApplyDamage()
    {
        if (health <= 0)
            return;
        airship.ReduceAltitude(altitudeLossPerSecond * Time.deltaTime);
    }

    void CheckHealth()
    {
        if (health > 0)
            return;
        Instantiate(patchToClone, gameObject.transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}
