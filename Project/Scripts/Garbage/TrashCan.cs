using Polyperfect.Common;
using TMPro;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public int score;
    public ScoreScriptableObject scoreScriptableObject;
    public GameObject deer;
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI textMeshProUGUI2;


    AudioSource audioSource;


    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();

        // Ref the score text
        GameObject yourObject = GameObject.FindWithTag("Moghet");
        GameObject yourObject2 = GameObject.FindWithTag("damn");

        if (yourObject != null && yourObject2!=null)
        {

            textMeshProUGUI = yourObject.GetComponent<TextMeshProUGUI>();
            textMeshProUGUI2 = yourObject2.GetComponent<TextMeshProUGUI>();

        }

        if (scoreScriptableObject.deerBought)
        {
            deer.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PhysicsObject>())
        {
            // Handle the trash item that entered the trash can.
            HandleTrashItem(other.gameObject);
        }
    }

    private void HandleTrashItem(GameObject trashItem)
    {
        // check if garbage is being holded and collided with the trash by any chance then ignore

        if (PlayerInteractions.Instance.isHolding)
            return;

        score += 5;
        textMeshProUGUI.text = score.ToString();
        scoreScriptableObject.score = score;

        if (score == 25)
            textMeshProUGUI2.text = "Bravo pour votre engagement et responsabilité. Grâce à toi, notre monde est plus beau.";

        PlaySound();
    }

    public int GetScore()
    {
        return score;
    }


    public void PlaySound()
    {
        audioSource.Play();
    }

}