using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject Player;

    // Start is called before the first frame update
    private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);

    }


}
