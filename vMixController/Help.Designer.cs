﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace vMixController {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Help {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Help() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("vMixController.Help", typeof(Help).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на If checked, color parameter applied not only for caption, but also for button body..
        /// </summary>
        public static string Button_Colorize {
            get {
                return ResourceManager.GetString("Button_Colorize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на If checked, button script will be executed right after UTC loaded..
        /// </summary>
        public static string Button_ExecuteAfterLoad {
            get {
                return ResourceManager.GetString("Button_ExecuteAfterLoad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на If checked, button will show status LED, according to script commands..
        /// </summary>
        public static string Button_StateDependent {
            get {
                return ResourceManager.GetString("Button_StateDependent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Fill list with data from another widget..
        /// </summary>
        public static string DataSource_Default {
            get {
                return ResourceManager.GetString("DataSource_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Here you can activate widget hotkeys.
        ///Name of the hotkey is self-explanatory.
        ///Event will be executed, when user press hotkey into layout panel, while no widget is focused.
        ///Link parameter is for calling events from scripts..
        /// </summary>
        public static string Hotkeys_Default {
            get {
                return ResourceManager.GetString("Hotkeys_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Edit items, that will be shown in widget dropdown list.
        ///Put ~ symbol to add description for list item..
        /// </summary>
        public static string List_Default {
            get {
                return ResourceManager.GetString("List_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Here you can assign MIDI commands to hotkeys in another widgets.
        ///Use P as parameter in button script to access fader/knob value..
        /// </summary>
        public static string Midi_Default {
            get {
                return ResourceManager.GetString("Midi_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Save current widget with properties as template for fast access..
        /// </summary>
        public static string SaveTemplate_Default {
            get {
                return ResourceManager.GetString("SaveTemplate_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Here you can make list of events, that will be executed at specified time and day of the week..
        /// </summary>
        public static string Scheduler_Default {
            get {
                return ResourceManager.GetString("Scheduler_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Here you can automate some actions, like toggling overlay or sending input to program/preview.
        ///Start with one command or create complex scripts with conditions..
        /// </summary>
        public static string Script_Default {
            get {
                return ResourceManager.GetString("Script_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Here you can create static variables, accessible from scripts.
        ///Variables started from &quot;@&quot; symbol treat as input variables.
        ///Input variables are accessible from title mappings and input selector into script command..
        /// </summary>
        public static string StaticVariables_Default {
            get {
                return ResourceManager.GetString("StaticVariables_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на If checked, text field of this widget will be working in `table` mode.
        ///It means, that all mapped title texts will be shown into text field with `|` delimiter.
        ///User can edit one title at time, or replace text in all titles.
        ///This option is useful for List Widget..
        /// </summary>
        public static string TextField_Table {
            get {
                return ResourceManager.GetString("TextField_Table", resourceCulture);
            }
        }
    }
}
