﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DailyEZ.Web.SearchService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Folder", Namespace="http://schemas.datacontract.org/2004/07/SearchServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class Folder : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> Parent_Folder_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DailyEZ.Web.SearchService.Page[] PagesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Parent_Folder_ID {
            get {
                return this.Parent_Folder_IDField;
            }
            set {
                if ((this.Parent_Folder_IDField.Equals(value) != true)) {
                    this.Parent_Folder_IDField = value;
                    this.RaisePropertyChanged("Parent_Folder_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public DailyEZ.Web.SearchService.Page[] Pages {
            get {
                return this.PagesField;
            }
            set {
                if ((object.ReferenceEquals(this.PagesField, value) != true)) {
                    this.PagesField = value;
                    this.RaisePropertyChanged("Pages");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Page", Namespace="http://schemas.datacontract.org/2004/07/SearchServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class Page : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Folder_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> Auto_OrderingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Footer_HTMLField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MetaKeysField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MetaDescField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DailyEZ.Web.SearchService.Link[] LinksField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int Folder_ID {
            get {
                return this.Folder_IDField;
            }
            set {
                if ((this.Folder_IDField.Equals(value) != true)) {
                    this.Folder_IDField = value;
                    this.RaisePropertyChanged("Folder_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public System.Nullable<bool> Auto_Ordering {
            get {
                return this.Auto_OrderingField;
            }
            set {
                if ((this.Auto_OrderingField.Equals(value) != true)) {
                    this.Auto_OrderingField = value;
                    this.RaisePropertyChanged("Auto_Ordering");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string Footer_HTML {
            get {
                return this.Footer_HTMLField;
            }
            set {
                if ((object.ReferenceEquals(this.Footer_HTMLField, value) != true)) {
                    this.Footer_HTMLField = value;
                    this.RaisePropertyChanged("Footer_HTML");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string MetaKeys {
            get {
                return this.MetaKeysField;
            }
            set {
                if ((object.ReferenceEquals(this.MetaKeysField, value) != true)) {
                    this.MetaKeysField = value;
                    this.RaisePropertyChanged("MetaKeys");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string MetaDesc {
            get {
                return this.MetaDescField;
            }
            set {
                if ((object.ReferenceEquals(this.MetaDescField, value) != true)) {
                    this.MetaDescField = value;
                    this.RaisePropertyChanged("MetaDesc");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public DailyEZ.Web.SearchService.Link[] Links {
            get {
                return this.LinksField;
            }
            set {
                if ((object.ReferenceEquals(this.LinksField, value) != true)) {
                    this.LinksField = value;
                    this.RaisePropertyChanged("Links");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Link", Namespace="http://schemas.datacontract.org/2004/07/SearchServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class Link : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Page_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool Is_LinkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string URLField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TargetField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Page_ID {
            get {
                return this.Page_IDField;
            }
            set {
                if ((this.Page_IDField.Equals(value) != true)) {
                    this.Page_IDField = value;
                    this.RaisePropertyChanged("Page_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Position {
            get {
                return this.PositionField;
            }
            set {
                if ((this.PositionField.Equals(value) != true)) {
                    this.PositionField = value;
                    this.RaisePropertyChanged("Position");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public bool Is_Link {
            get {
                return this.Is_LinkField;
            }
            set {
                if ((this.Is_LinkField.Equals(value) != true)) {
                    this.Is_LinkField = value;
                    this.RaisePropertyChanged("Is_Link");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string URL {
            get {
                return this.URLField;
            }
            set {
                if ((object.ReferenceEquals(this.URLField, value) != true)) {
                    this.URLField = value;
                    this.RaisePropertyChanged("URL");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string Target {
            get {
                return this.TargetField;
            }
            set {
                if ((object.ReferenceEquals(this.TargetField, value) != true)) {
                    this.TargetField = value;
                    this.RaisePropertyChanged("Target");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchResult", Namespace="http://schemas.datacontract.org/2004/07/SearchServiceLibrary.Models")]
    [System.SerializableAttribute()]
    public partial class SearchResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Link_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Link_TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Link_URLField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Page_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Page_TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Link_ID {
            get {
                return this.Link_IDField;
            }
            set {
                if ((this.Link_IDField.Equals(value) != true)) {
                    this.Link_IDField = value;
                    this.RaisePropertyChanged("Link_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Link_Title {
            get {
                return this.Link_TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.Link_TitleField, value) != true)) {
                    this.Link_TitleField = value;
                    this.RaisePropertyChanged("Link_Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Link_URL {
            get {
                return this.Link_URLField;
            }
            set {
                if ((object.ReferenceEquals(this.Link_URLField, value) != true)) {
                    this.Link_URLField = value;
                    this.RaisePropertyChanged("Link_URL");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Page_ID {
            get {
                return this.Page_IDField;
            }
            set {
                if ((this.Page_IDField.Equals(value) != true)) {
                    this.Page_IDField = value;
                    this.RaisePropertyChanged("Page_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Page_Title {
            get {
                return this.Page_TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.Page_TitleField, value) != true)) {
                    this.Page_TitleField = value;
                    this.RaisePropertyChanged("Page_Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Ad_Assignment", Namespace="http://schemas.datacontract.org/2004/07/SearchServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class Ad_Assignment : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> Ad_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Ad_GroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Client_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Height_LimitationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double View_PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Ad_ModeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Ad_LimitField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public System.Nullable<int> Ad_ID {
            get {
                return this.Ad_IDField;
            }
            set {
                if ((this.Ad_IDField.Equals(value) != true)) {
                    this.Ad_IDField = value;
                    this.RaisePropertyChanged("Ad_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int Ad_Group {
            get {
                return this.Ad_GroupField;
            }
            set {
                if ((this.Ad_GroupField.Equals(value) != true)) {
                    this.Ad_GroupField = value;
                    this.RaisePropertyChanged("Ad_Group");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int Client_ID {
            get {
                return this.Client_IDField;
            }
            set {
                if ((this.Client_IDField.Equals(value) != true)) {
                    this.Client_IDField = value;
                    this.RaisePropertyChanged("Client_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int Height_Limitation {
            get {
                return this.Height_LimitationField;
            }
            set {
                if ((this.Height_LimitationField.Equals(value) != true)) {
                    this.Height_LimitationField = value;
                    this.RaisePropertyChanged("Height_Limitation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public double View_Price {
            get {
                return this.View_PriceField;
            }
            set {
                if ((this.View_PriceField.Equals(value) != true)) {
                    this.View_PriceField = value;
                    this.RaisePropertyChanged("View_Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public int Ad_Mode {
            get {
                return this.Ad_ModeField;
            }
            set {
                if ((this.Ad_ModeField.Equals(value) != true)) {
                    this.Ad_ModeField = value;
                    this.RaisePropertyChanged("Ad_Mode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public int Ad_Limit {
            get {
                return this.Ad_LimitField;
            }
            set {
                if ((this.Ad_LimitField.Equals(value) != true)) {
                    this.Ad_LimitField = value;
                    this.RaisePropertyChanged("Ad_Limit");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Ads_Model", Namespace="http://schemas.datacontract.org/2004/07/SearchServiceLibrary")]
    [System.SerializableAttribute()]
    public partial class Ads_Model : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Client_IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime Date_CreatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int Ad_HeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Border_StyleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DailyEZ.Web.SearchService.Ad_Assignment[] Ad_AssignmentsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int Client_ID {
            get {
                return this.Client_IDField;
            }
            set {
                if ((this.Client_IDField.Equals(value) != true)) {
                    this.Client_IDField = value;
                    this.RaisePropertyChanged("Client_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public System.DateTime Date_Created {
            get {
                return this.Date_CreatedField;
            }
            set {
                if ((this.Date_CreatedField.Equals(value) != true)) {
                    this.Date_CreatedField = value;
                    this.RaisePropertyChanged("Date_Created");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int Ad_Height {
            get {
                return this.Ad_HeightField;
            }
            set {
                if ((this.Ad_HeightField.Equals(value) != true)) {
                    this.Ad_HeightField = value;
                    this.RaisePropertyChanged("Ad_Height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string Html {
            get {
                return this.HtmlField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlField, value) != true)) {
                    this.HtmlField = value;
                    this.RaisePropertyChanged("Html");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string Border_Style {
            get {
                return this.Border_StyleField;
            }
            set {
                if ((object.ReferenceEquals(this.Border_StyleField, value) != true)) {
                    this.Border_StyleField = value;
                    this.RaisePropertyChanged("Border_Style");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public DailyEZ.Web.SearchService.Ad_Assignment[] Ad_Assignments {
            get {
                return this.Ad_AssignmentsField;
            }
            set {
                if ((object.ReferenceEquals(this.Ad_AssignmentsField, value) != true)) {
                    this.Ad_AssignmentsField = value;
                    this.RaisePropertyChanged("Ad_Assignments");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SearchService.ISearchService")]
    public interface ISearchService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/GetAllSubFolders", ReplyAction="http://tempuri.org/ISearchService/GetAllSubFoldersResponse")]
        DailyEZ.Web.SearchService.Folder[] GetAllSubFolders(int parentID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/Search", ReplyAction="http://tempuri.org/ISearchService/SearchResponse")]
        DailyEZ.Web.SearchService.SearchResult[] Search(int[] folders, [System.ServiceModel.MessageParameterAttribute(Name="search")] string search1);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/SearchAds", ReplyAction="http://tempuri.org/ISearchService/SearchAdsResponse")]
        DailyEZ.Web.SearchService.Ad_Assignment[] SearchAds(string search);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISearchService/GetAd", ReplyAction="http://tempuri.org/ISearchService/GetAdResponse")]
        DailyEZ.Web.SearchService.Ads_Model GetAd(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISearchServiceChannel : DailyEZ.Web.SearchService.ISearchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SearchServiceClient : System.ServiceModel.ClientBase<DailyEZ.Web.SearchService.ISearchService>, DailyEZ.Web.SearchService.ISearchService {
        
        public SearchServiceClient() {
        }
        
        public SearchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SearchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public DailyEZ.Web.SearchService.Folder[] GetAllSubFolders(int parentID) {
            return base.Channel.GetAllSubFolders(parentID);
        }
        
        public DailyEZ.Web.SearchService.SearchResult[] Search(int[] folders, string search1) {
            return base.Channel.Search(folders, search1);
        }
        
        public DailyEZ.Web.SearchService.Ad_Assignment[] SearchAds(string search) {
            return base.Channel.SearchAds(search);
        }
        
        public DailyEZ.Web.SearchService.Ads_Model GetAd(int id) {
            return base.Channel.GetAd(id);
        }
    }
}
