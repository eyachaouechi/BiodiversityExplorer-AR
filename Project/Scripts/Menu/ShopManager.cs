using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public ScoreScriptableObject scoreScript;
    public TextMeshProUGUI currencyText;
    public GameObject BtnBought;
    public TextMeshProUGUI hint; 

    // Start is called before the first frame update
    void Start()
    {
        // print current currency 
        if (currencyText != null)       
        currencyText.text = scoreScript.score.ToString();


        if (BtnBought == null)
            return;

        if(scoreScript.deerBought) 
            BtnBought.SetActive(false);
    }


    public void LoadArSceneForAnimal(string animal)
    {
        SceneManager.LoadScene(animal);
    }


    public void UpdateCurrency()
    {

        if (scoreScript.score < 10)
        {
            hint.text = "Vous n'avez pas assez d'argent.";
            Invoke("DisableText", 1f);
        } else
        {
            //has money
            scoreScript.score = 0;
            currencyText.text = scoreScript.score.ToString();
            scoreScript.deerBought = true;

            print(scoreScript.deerBought);

            hint.text = "Félicitations, vous avez débloqué un nouvel animal : M. Cerf-Kun. consultez votre parc pour le voir";
        }

   
    }


    void DisableText()
    {
        // Disable the Text component
        hint.text = "";
    }
}
