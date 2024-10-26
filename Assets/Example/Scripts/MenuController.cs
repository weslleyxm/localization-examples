using System.Collections; 
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuItem[] menuItems;
    private int menuIndex;
    private int lastIndex;
    private bool dpadVerticalBlocked = false;
    private bool dpadHorizontalBlocked = false; 

    private void Start()
    {
        menuItems[0].Select();
    }
   
    private IEnumerator BlockVerticalDpad()
    {
        if (!dpadVerticalBlocked)
        {
            dpadVerticalBlocked = true;
            yield return new WaitForSeconds(.16f);
            dpadVerticalBlocked = false;
        }

    }
    private IEnumerator BlockHorizontalDpad() 
    {
        if (!dpadVerticalBlocked)
        {
            dpadHorizontalBlocked = true;
            yield return new WaitForSeconds(.16f);
            dpadHorizontalBlocked = false;
        } 
    } 

    private void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
           Input.GetAxis("DPadVertical") == 1 && dpadVerticalBlocked == false)
        {
            MenuManager.Instance.StartCoroutine(BlockVerticalDpad());
            menuIndex--; 

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Input.GetAxis("DPadVertical") == -1 && dpadVerticalBlocked == false) 
        {
            MenuManager.Instance.StartCoroutine(BlockVerticalDpad());
            menuIndex++;
        } 

        if (menuIndex > menuItems.Length - 1)
            menuIndex = 0;
        if (menuIndex < 0)
            menuIndex = menuItems.Length - 1;

        if (menuIndex != lastIndex)
        { 
            menuItems[lastIndex].Deselect();
            menuItems[menuIndex].Select();

            lastIndex = menuIndex;
        }


        if (menuItems[menuIndex].useArrowToInteract)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.JoystickButton0) ||
                Input.GetAxis("DPadHorizontal") == -1 && dpadHorizontalBlocked == false)
            {
                MenuManager.Instance.StartCoroutine(BlockHorizontalDpad()); 
                menuItems[menuIndex]?.OnPressMenuItemEventHandler.Invoke(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.JoystickButton0) ||
                Input.GetAxis("DPadHorizontal") == 1 && dpadHorizontalBlocked == false)
            {
                MenuManager.Instance.StartCoroutine(BlockHorizontalDpad()); 
                menuItems[menuIndex]?.OnPressMenuItemEventHandler.Invoke(Direction.Right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) ||
                 Input.GetKeyDown(KeyCode.Return) ||
                 Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            menuItems[menuIndex]?.OnPressMenuItemEventHandler.Invoke(Direction.Right);
        }

    }
}
