using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static int MaxScore = -1;

    public Vector3 spawnValues;

    public GameObject[] SpawnPositions;

    public Enemy HardEnemy;
    public Enemy MediumEnemy;
    public Enemy EasyEnemy;

    public bool isGameOver = false;
    public int score;

    public TextMeshProUGUI scoreTextMesh;
    public PlayerController player;

    private CameraShake cameraShake;

    void Start()
    {
        score = 0;
        cameraShake = Camera.main.GetComponent<CameraShake>();

        StartCoroutine(SpawnEnemies(6, 100, 1000, HardEnemy));
        StartCoroutine(SpawnEnemies(5, 1, 10, EasyEnemy));
        StartCoroutine(SpawnEnemies(4, 1, 10, EasyEnemy));
        StartCoroutine(SpawnEnemies(6, 1, 10, EasyEnemy));
        StartCoroutine(SpawnEnemies(10, 1, 10, EasyEnemy));
        StartCoroutine(SpawnEnemies(10, 1, 10, EasyEnemy));

        player.OnCollision += () => cameraShake.Shake(0.5f, 2);
        player.OnDie += GameOver;

        UpdateScore();
    }

    IEnumerator SpawnEnemies(float wait, int damagedPoints, int diePoints, Enemy originalEnem)
    {
        while (true)
        {
            (var spawnPosition, var spawnRotation) = GetRandomSpawnPosition();

            var enemy = Instantiate(originalEnem, spawnPosition, spawnRotation);

            enemy.OnDamaged += (_) => { AddScore(damagedPoints); };
            enemy.OnDie += () =>
            {
                AddScore(diePoints);
            };

            yield return new WaitForSeconds(wait);
        }
    }

    private (Vector3, Quaternion) GetRandomSpawnPosition()
    {
        var spawnValues = SpawnPositions[Random.Range(0, SpawnPositions.Length)];

        var position = spawnValues.transform.position;
        //position = new Vector3(Random.Range(-position.x, position.x), position.y, position.z);
        return (position, spawnValues.transform.rotation);
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreTextMesh.text = score.ToString();
    }

    public void GameOver()
    {
        MaxScore = Mathf.Max(score, MaxScore);
        SceneManager.LoadScene("Menu");
    }
}
