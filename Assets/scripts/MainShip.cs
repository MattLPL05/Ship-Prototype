using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

//put everything in one script basicly.. i could seperate everything into even smaller modules, but that feels unnecessary here
[RequireComponent(typeof(Rigidbody))]
public class MainShip : MonoBehaviour
{
    //Create our variables
    [SerializeField]
    private int Opened;

    [SerializeField]
    private float SailRotation;

    [SerializeField]
    private GameObject RotorgameObject;

    [SerializeField]
    private Transform WindObject;

    private float WindValue = 1f; //set a default of 1f so we wont become solf locked


    //init our inputs
    [SerializeField]
    private PlayerInput playerInput;
    

    //select the UI renderer
    [SerializeField]
    private PanelRenderer PanelRenderer;

    //private Labels, so we can just spam Label updates once we point to them
    private Label OpenedUI;
    private Label SailRotationUI;

    //required for movement
    private Rigidbody rb;
    private float CurrentShipRotation;


    void Start()
    {
        //get the UI doc
        var uiDocument = PanelRenderer.GetComponent<UIDocument>();

        OpenedUI = uiDocument.rootVisualElement.Q<Label>("SailOpen");
        SailRotationUI = uiDocument.rootVisualElement.Q<Label>("SailRotor");

        //initial Update of the variables
        OpenedUI.text = "Sail Opened: " + Opened.ToString() + "%";
        SailRotationUI.text = "Sail Rotation: " + SailRotation.ToString() + " DEG";

        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 1;

    }

    private void UpdateUI()
    {
        OpenedUI.text = "Sail Opened: " + Opened.ToString() + "%";
        SailRotationUI.text = "Sail Rotation: " + SailRotation.ToString() + " DEG";
    }

    private void UpdateSpeed() 
    {
        rb.AddForce((transform.forward * Opened) * WindValue / rb.mass, ForceMode.Impulse);
        
    }

    private void UpdateRotor()
    {
        
        CurrentShipRotation = this.transform.localEulerAngles.y;
        if(Opened == 0)
        {
            return;
        }
        this.transform.rotation = Quaternion.Euler(0, (CurrentShipRotation + (SailRotation/180)/45) , 0);
        
    }


    //input handling. In a very basic copy-paste form
    public void OnOpen(InputAction.CallbackContext context)
    {
        //We run this only, AND ONLY if we didn't reach the limits
        if(Opened > 100 || Opened < 0)
        {
            //if we somehow go further than limits, reset
            Opened = 0;
            UpdateUI();
        }
        else if(Opened == 100)
        {
            //nothing. There is a better way to do this probably, but i don't remember it right now
        }
        else
        {
            //do the thing!
            Opened = Opened + 1;
            UpdateUI();
        }
        
    }

    public void OnClose(InputAction.CallbackContext context)
    {
        //We run this only, AND ONLY if we didn't reach the limits
        if(Opened > 100 || Opened < 0)
        {
            //if we somehow go further than limits, reset
            Opened = 0;
            UpdateUI();
        }
        else if(Opened == 0)
        {
            //nothing. There is a better way to do this probably, but i don't remember it right now
        }
        else
        {
            //do the thing!
            Opened = Opened - 1;
            UpdateUI();
        }
    }

    public void RotateLeft(InputAction.CallbackContext context)
    {
        //We run this only, AND ONLY if we didn't reach the limits
        if(SailRotation > 45 || SailRotation < -45)
        {
            //if we somehow go further than limits, reset
            SailRotation = 0;
            UpdateUI();
        }
        else if(SailRotation == 45)
        {
            //nothing. There is a better way to do this probably, but i don't remember it right now
        }
        else
        {
            //do the thing!
            SailRotation = SailRotation + 0.5f;
            UpdateUI();
            RotorgameObject.transform.localRotation = Quaternion.Euler(0, SailRotation, 0); //update the model's rotation
        }
        
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        //We run this only, AND ONLY if we didn't reach the limits
        if(SailRotation > 45 || SailRotation < -45)
        {
            //if we somehow go further than limits, reset
            SailRotation = 0;
            UpdateUI();
        }
        else if(SailRotation == -45)
        {
            //nothing. There is a better way to do this probably, but i don't remember it right now
        }
        else
        {
            //do the thing!
            SailRotation = SailRotation - 0.5f;
            UpdateUI();
            RotorgameObject.transform.localRotation = Quaternion.Euler(0, SailRotation, 0);
        }
        
    }

    private void UpdateWindEffectivness()
    {
        float angleDifference = Quaternion.Angle(WindObject.rotation, RotorgameObject.transform.rotation);

        if (angleDifference < 30.2f)
        {
            // The two rotations are very close
            WindValue = 1f;
        }
        else if (angleDifference > 30.2f && angleDifference > 65.2f)
        {
            WindValue = 0.5f;
        }
        else
        {
            WindValue = 0f;
        }
    }
   
    void Update()
    {
        UpdateSpeed();
        UpdateRotor();
        UpdateWindEffectivness();
    }
}
