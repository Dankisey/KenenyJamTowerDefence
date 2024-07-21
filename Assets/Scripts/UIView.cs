using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private EnemyFactory _factory;
    [SerializeField] private Text _scoreTextText;
    [SerializeField] private Text _healthText;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private Text _failPanelText;
    [SerializeField] private Text _best;

    private bool _isAlive = true;
    private int _record = 0;
    private int _health = 3;

    private void Awake()
    {
        _failPanel.SetActive(false);
    }

    private void OnEnable()
    {
        _scoreTextText.text = $"Record: {_record}";
        _healthText.text = $"Health: {_health}";
        _factory.GoldEarned += OnEnemyKilled;
        _factory.EnemyFinished += OnEnemyFinished;
    }

    private void OnDisable()
    {
        _factory.GoldEarned -= OnEnemyKilled;
        _factory.EnemyFinished -= OnEnemyFinished;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnemyKilled(int add)
    {
        if (_isAlive == false)
            return;

        _record += add;
        _scoreTextText.text = $"Record: {_record}";
    }

    private void OnEnemyFinished()
    {
        if (_isAlive == false)
            return;

        _health--;
        _isAlive = _health > 0;

        _healthText.text = $"Health: {_health}";

        if (_isAlive == false)
        {
            Time.timeScale = 0;
            _failPanel.SetActive(true);
            _failPanelText.text = $"Your score is: {_record}";

            if (PlayerPrefs.HasKey(nameof(_record)))
            {
                _best.text = $"Your best is: {PlayerPrefs.GetInt(nameof(_record))}";

                if (_record > PlayerPrefs.GetInt(nameof(_record)))
                    PlayerPrefs.SetInt(nameof(_record), _record);
            }
            else
            {
                _best.text = $"Your best is: None";
                PlayerPrefs.SetInt(nameof(_record), _record);
            }

            PlayerPrefs.Save();
        }
    }
}