using System.Collections.Generic;
using Fjord.Modules.Debug;

namespace Fjord.src.Modules.UI;

// {
//     header {
//         label: "Player Inspector"
//     },
//     container {
//         label: "Position",
//         expanded: false,
//         components: {
//             slider {
//                 label: "X",
//                 value: 0
//             },
//             slider {
//                 label: "Y",
//                 value: 0
//             },
//             slider {
//                 label: "Z",
//                 value: 0
//             }
//         }
//     }
// }

interface UIComponent {}

struct UIContainer : UIComponent {
    public string label;
    public bool expanded = true;
    public List<UIComponent> components;

    public UIContainer(string label, bool expanded, List<UIComponent> components) {
        this.label = label;
        this.expanded = expanded;
        this.components = components;
    }
}

struct UIButton : UIComponent {}
struct UICheckbox : UIComponent {}
struct UIDropdown : UIComponent {}
struct UISlider : UIComponent {
    public string label;
    public int value;
}
struct UISwitch : UIComponent {}
struct UIInputField : UIComponent {}
struct UITextBox : UIComponent {}
struct UIHeader : UIComponent {}

public static class FUI
{
    private static Dictionary<string, UIContainer> _uiDictionary = new Dictionary<string, UIContainer>();
    
    internal static void renderContainer(UIContainer targetContainer) {
        foreach(UIComponent component in targetContainer.components) {
            Debug.Send(component.GetType());
        }
    }

    public static void init() {
        UIContainer UI = new UIContainer() {
            label = "PlayerUI",
            components = new List<UIComponent>() {
                new UIContainer() {
                    label = "Position",
                    components = new List<UIComponent>() {
                        new UISlider() {
                            label = "X",
                            value = 0
                        },
                        new UISlider() {
                            label = "Y",
                            value = 0
                        },
                        new UISlider() {
                            label = "Z",
                            value = 0
                        }
                    }
                },
                new UIContainer() {
                    label = "Rigidbody",
                    components = new List<UIComponent>() {
                        new UISlider() {
                            label = "Gravity",
                            value = 0
                        },
                        new UISlider() {
                            label = "Slippery",
                            value = 0
                        }
                    }
                }
            } 
        };

        renderContainer(UI);
    }
}