﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestProject.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TestProject.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error ocurred.
        /// </summary>
        internal static string DefaultErrorMessage {
            get {
                return ResourceManager.GetString("DefaultErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The username is already taken.
        /// </summary>
        internal static string DuplicatedUsername {
            get {
                return ResourceManager.GetString("DuplicatedUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The game is not started yet.
        /// </summary>
        internal static string GameNotStarted {
            get {
                return ResourceManager.GetString("GameNotStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The birthdate is not valid.
        /// </summary>
        internal static string InvalidBirthDate {
            get {
                return ResourceManager.GetString("InvalidBirthDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The board is not valid.
        /// </summary>
        internal static string InvalidBoard {
            get {
                return ResourceManager.GetString("InvalidBoard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The board is not valid for the user.
        /// </summary>
        internal static string InvalidBoardForCurrentUser {
            get {
                return ResourceManager.GetString("InvalidBoardForCurrentUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The cell is not valid in the board.
        /// </summary>
        internal static string InvalidCell {
            get {
                return ResourceManager.GetString("InvalidCell", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The username is not valid.
        /// </summary>
        internal static string InvalidUsername {
            get {
                return ResourceManager.GetString("InvalidUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User registered properly.
        /// </summary>
        internal static string UserRegistered {
            get {
                return ResourceManager.GetString("UserRegistered", resourceCulture);
            }
        }
    }
}