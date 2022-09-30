using System.Collections.Generic;
using System.Numerics;
using Fjord.Modules.Debug;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.UI;

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

public interface UIComponent {}

public struct UIContainer : UIComponent {
    public string label;
    public bool expanded = true;
    public List<UIComponent> components;

    public UIContainer(string label, bool expanded, List<UIComponent> components) {
        this.label = label;
        this.expanded = expanded;
        this.components = components;
    }
}

public struct UIButton : UIComponent {}
public struct UICheckbox : UIComponent {}
public struct UIDropdown : UIComponent {}
public struct UISlider : UIComponent {
    public string label;
    public int value;
}
public struct UISwitch : UIComponent {}
public struct UIInputField : UIComponent {}
public struct UITextBox : UIComponent {}
public struct UIHeader : UIComponent {}

public class UIWindow {
    public UIContainer UI;
    public int Yoffset;
    public int depth;
    public int depthWidth = 10;

    public int fontSize = 15;
    public string font = "OpenSans";
    
    public Vector4 fgColor = new Vector4(255, 255, 255, 255);

    public Dictionary<string, bool> UIDisplayList = new Dictionary<string, bool>();
}

public static class FUI
{
    private static Dictionary<string, UIWindow> _uiDictionary = new Dictionary<string, UIWindow>();
    
    internal static void renderContainer(UIContainer targetContainer, string ID) {  
        if(!_uiDictionary[ID].UIDisplayList.ContainsKey(targetContainer.GetHashCode().ToString())) {
            _uiDictionary[ID].UIDisplayList.Add(targetContainer.GetHashCode().ToString(), false);
        }

        Draw.Text(new Vector2(_uiDictionary[ID].depth * _uiDictionary[ID].depthWidth, _uiDictionary[ID].Yoffset), _uiDictionary[ID].font, _uiDictionary[ID].fontSize, targetContainer.label, _uiDictionary[ID].fgColor, 0);
        // _uiDictionary[ID].Yoffset += (int)Draw.GetTextRectangle(new Vector2(_uiDictionary[ID].depth * _uiDictionary[ID].depthWidth, _uiDictionary[ID].Yoffset) , _uiDictionary[ID].font, _uiDictionary[ID].fontSize, targetContainer.label).Y;
        _uiDictionary[ID].Yoffset += _uiDictionary[ID].fontSize + 4;
        if(!_uiDictionary[ID].UIDisplayList[targetContainer.GetHashCode().ToString()]) {
            return;
        }
       
        _uiDictionary[ID].depth += 1;

        foreach(UIComponent _component in targetContainer.components) {
            if(_component.GetType() == typeof(UIContainer)) {
                var component = (UIContainer)_component;

                if(!_uiDictionary[ID].UIDisplayList.ContainsKey(component.GetHashCode().ToString())) {
                    _uiDictionary[ID].UIDisplayList.Add(component.GetHashCode().ToString(), true);
                }

                // Draw.Text(new Vector2(_uiDictionary[ID].depth * _uiDictionary[ID].depthWidth, _uiDictionary[ID].Yoffset), _uiDictionary[ID].font, _uiDictionary[ID].fontSize, component.label, _uiDictionary[ID].fgColor, 0);
            
                if(_uiDictionary[ID].UIDisplayList[component.GetHashCode().ToString()]) {
                    renderContainer((UIContainer)component, ID);
                }
            } else if(_component.GetType() == typeof(UISlider)) {
                var component = (UISlider)_component;

                Draw.Text(new Vector2(_uiDictionary[ID].depth * _uiDictionary[ID].depthWidth, _uiDictionary[ID].Yoffset), _uiDictionary[ID].font, _uiDictionary[ID].fontSize, component.label, _uiDictionary[ID].fgColor, 0);
                _uiDictionary[ID].Yoffset += (int)Draw.GetTextRectangle(Vector2.Zero, _uiDictionary[ID].font, _uiDictionary[ID].fontSize, component.label).W;
            }
        }
        if(_uiDictionary[ID].depth > 0)
            _uiDictionary[ID].depth -= 1;
    }

    public static void RegisterWindow(UIContainer Container, string ID) {
        _uiDictionary.Add(ID, new UIWindow() {
            UI = Container,
            Yoffset = 0,
            depth = 0
        });
    }

    public static void RenderWindows() {
        foreach(KeyValuePair<string, UIWindow> kvp in _uiDictionary) {
            _uiDictionary[kvp.Key].Yoffset = 0;
            renderContainer(kvp.Value.UI, kvp.Key);
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
                        },
                        new UIContainer() {
                            label = "Layer 2 Test",
                            components = new List<UIComponent>() {
                                new UISlider() {
                                    label = "Test",
                                    value = 0
                                },
                                new UISlider() {
                                    label = "Slider",
                                    value = 0
                                },
                                new UIContainer() {
                                    label = "Layer 3 Test",
                                    components = new List<UIComponent>() {
                                        new UISlider() {
                                            label = "Test slider 2",
                                            value = 0
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new UIContainer() {
                    label = "Rigidbody",
                    components = new List<UIComponent>() {
                        new UISlider() {
                            label = "Gravity",
                            value = 0
                        }
                    }
                }
            } 
        };

        RegisterWindow(UI, "playerUI");
    }
}