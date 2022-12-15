using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI scoreText;

    private int score;

    private void Awake() {
        Debug.Log("called");
        // foreach(Component component in FindObjectOfType<Canvas>().gameObject.transform.GetChild(0).GetComponents<Component>()){
        //     Debug.Log(component.GetType());
        // }
        scoreText = FindObjectOfType<Canvas>().gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void NewGame(){
        score = 0;
        scoreText.text = score.ToString();
    }
    public void IncreaseScore(){
        score++;
        scoreText.text = score.ToString();
    }

    void Start(){
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
