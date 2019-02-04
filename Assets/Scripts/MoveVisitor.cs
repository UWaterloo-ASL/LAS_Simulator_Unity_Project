using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVisitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            var rotation = this.transform.rotation;
            position.x--;
            rotation.y = -90;
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            var rotation = this.transform.rotation;
            position.x++;
            rotation.y = 90;
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            var rotation = this.transform.rotation;
            position.z++;
            rotation.y = 0;
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            var rotation = this.transform.rotation;
            position.z--;
            rotation.y = 180;
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
