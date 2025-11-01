using UnityEngine;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{
    UIDocument UIDocument;
    public GameObject VHS_Plane;
    public RigidbodyMovement PlayerMovement;
    public BodycamVHSController BodycamVHSController;

    VisualElement panel;
    Toggle VHSToggle;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        var root = UIDocument.rootVisualElement;
        panel = root.Q<VisualElement>("Options-Menu-Panel");
        VHSToggle = root.Q<Toggle>("VHS-Toggle");
        VHSToggle.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("VHS Toggle changed to: " + evt.newValue);
            // Here you would add the logic to enable/disable the VHS effect
            VHS_Plane.SetActive(evt.newValue);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Pause)
        {
            panel.style.display = panel.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            
            // Disable player movement when the options menu is open
            PlayerMovement.enabled = panel.style.display == DisplayStyle.None;
            BodycamVHSController.enabled = panel.style.display == DisplayStyle.None;

            // Cursor visibility and lock state
            UnityEngine.Cursor.visible = panel.style.display == DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = panel.style.display == DisplayStyle.Flex ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
