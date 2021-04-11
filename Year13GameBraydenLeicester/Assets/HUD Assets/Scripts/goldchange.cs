using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldchange : MonoBehaviour
{
    public Text txt;
    public float goldAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) { goldAmount += 1; }
        if (Input.GetKeyDown(KeyCode.K)) { goldAmount -= 1; }
        txt.text = goldAmount.ToString();
    }
}
