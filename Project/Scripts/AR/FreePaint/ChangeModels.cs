using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModels : MonoBehaviour
{
    [SerializeField]
    private GameObject currentModel;
    [SerializeField]
    private GameObject brushContainer;

    [SerializeField]
    private GameObject biblio;

    [SerializeField]
    private GameObject edit;

    public void ActiveCurrentModel(GameObject model)
    {

        if (model == currentModel)
            return;

        ClearPainted();
        currentModel.SetActive(false);
        model.SetActive(true);
        currentModel = model;
    }


    void ClearPainted()
    {
        foreach (Transform child in brushContainer.transform)
        {//Clear brushes
            Destroy(child.gameObject);
        }
    }


    public void FlipFlopBilio()
    {
        if(edit.activeSelf)
            edit.SetActive(false); 

        biblio.SetActive(!biblio.activeSelf);

    }

    public void FlipFlopEdit()
    {
        if (biblio.activeSelf)
            biblio.SetActive(false);

        edit.SetActive(!edit.activeSelf);

    }

}



