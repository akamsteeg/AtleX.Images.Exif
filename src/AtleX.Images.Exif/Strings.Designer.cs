﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtleX.Images.Exif {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AtleX.Images.Exif.Strings", typeof(Strings).Assembly);
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
        ///   Looks up a localized string similar to An integer is either 2 or 4 bytes long.
        /// </summary>
        internal static string ExceptionCantConvertBytesToInteger {
            get {
                return ResourceManager.GetString("ExceptionCantConvertBytesToInteger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t find file &apos;{0}&apos;.
        /// </summary>
        internal static string ExceptionFileNotFound {
            get {
                return ResourceManager.GetString("ExceptionFileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data is not from a valid JPEG.
        /// </summary>
        internal static string ExceptionImageInvalidJpeg {
            get {
                return ResourceManager.GetString("ExceptionImageInvalidJpeg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t read from invalid or unopened data. Have you instantiated this reader with valid data?.
        /// </summary>
        internal static string ExceptionReaderCanNotRead {
            get {
                return ResourceManager.GetString("ExceptionReaderCanNotRead", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The first byte of a segment code should be 0xFF.
        /// </summary>
        internal static string ExceptionSegmentInvalidFirstByte {
            get {
                return ResourceManager.GetString("ExceptionSegmentInvalidFirstByte", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A segment code must be two bytes long.
        /// </summary>
        internal static string ExceptionSegmentTypeIsTwoBytes {
            get {
                return ResourceManager.GetString("ExceptionSegmentTypeIsTwoBytes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t read past the end of the source.
        /// </summary>
        internal static string ExceptionSourceInvalidLength {
            get {
                return ResourceManager.GetString("ExceptionSourceInvalidLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data is not from a supported image.
        /// </summary>
        internal static string ExceptionUnsupportedImageData {
            get {
                return ResourceManager.GetString("ExceptionUnsupportedImageData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value can not be less than zero.
        /// </summary>
        internal static string ExceptionValueCanNotBeLessThanZero {
            get {
                return ResourceManager.GetString("ExceptionValueCanNotBeLessThanZero", resourceCulture);
            }
        }
    }
}
