using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float speedMin = 12;
    private float speedMax = 15;
    private float torqueRange = 10;
    private float xSpawnRange = 4;
    private float ySpawn = -2;

    public ParticleSystem explosionParticle;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown() {
        if (!gameManager.isGameActive) {
            return;
        }
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Sensor")) {
            if (!gameObject.CompareTag("Bad")) {
                gameManager.GameOver();
            }
            Destroy(gameObject);
        }
    }

    float RandomTorque() {
        return Random.Range(-torqueRange, torqueRange);
    }

    Vector3 RandomForce() {
        return Vector3.up * Random.Range(speedMin, speedMax);
    }

    Vector3 RandomSpawnPos() {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawn);
    }
}
