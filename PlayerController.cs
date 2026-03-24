using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 private Rigidbody rb; 
 private float movementX;
 private float movementY;

 private int count;


 public float speed = 0; 

 public TextMeshProUGUI countText;
 public GameObject winTextObject;


 void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; 
        SetCountText();
        winTextObject.SetActive(false);
    }
 
 // Called when a move input is detected
 void OnMove(InputValue movementValue)
    {
 // Convert the input value into a Vector2 for movement
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

      void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 14)
       {
           winTextObject.SetActive(true);
           Destroy(GameObject.FindGameObjectWithTag("Enemy"));
       }
   }


 void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);  // Create a 3D movement vector using the X and Y inputs
        rb.AddForce(movement * speed);   // Apply force to the Rigidbody to move the player
    }

 void OnTriggerEnter(Collider other) 
    { 
 // Check if the object the player collided with has the "PickUp" tag
 if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);  // Deactivate the collided object (making it disappear)
            count = count + 1;
            SetCountText();
        }
    }
     
     private void OnCollisionEnter(Collision collision)
{
   if (collision.gameObject.CompareTag("Enemy"))
   {
       // Destroy the current object
       Destroy(gameObject); 
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
   }
}
     

}