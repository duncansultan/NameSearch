﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NameSearch.App.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NameSearch.App.Tests.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {
        ///   &quot;count_person&quot;:1,
        ///   &quot;person&quot;:[
        ///      {
        ///         &quot;id&quot;:&quot;Person.fbb71b50-0000-4b57-aba0-eafef8ce9c57.Durable&quot;,
        ///         &quot;name&quot;:&quot;Giaan Qiuntero&quot;,
        ///         &quot;firstname&quot;:&quot;Giaan&quot;,
        ///         &quot;middlename&quot;:null,
        ///         &quot;lastname&quot;:&quot;Qiuntero&quot;,
        ///         &quot;age_range&quot;:&quot;60-64&quot;,
        ///         &quot;gender&quot;:&quot;Male&quot;,
        ///         &quot;found_at_address&quot;:{
        ///            &quot;id&quot;:&quot;Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable&quot;,
        ///            &quot;location_type&quot;:&quot;Address&quot;,
        ///            &quot;street_line_1&quot;:&quot;102 Syrws St&quot;,
        ///            &quot;str [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestResponseJson {
            get {
                return ResourceManager.GetString("TestResponseJson", resourceCulture);
            }
        }
    }
}