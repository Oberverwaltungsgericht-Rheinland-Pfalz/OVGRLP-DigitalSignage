﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// Dieser Quellcode wurde automatisch generiert von xsd, Version=4.6.1055.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Terminsaushang {
    
    private TerminsaushangStammdaten stammdatenField;
    
    private TerminsaushangVerfahren[] terminiertField;
    
    /// <remarks/>
    public TerminsaushangStammdaten Stammdaten {
        get {
            return this.stammdatenField;
        }
        set {
            this.stammdatenField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Verfahren", IsNullable=false)]
    public TerminsaushangVerfahren[] Terminiert {
        get {
            return this.terminiertField;
        }
        set {
            this.terminiertField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangStammdaten {
    
    private string gerichtsnameField;
    
    private string datumField;
    
    /// <remarks/>
    public string Gerichtsname {
        get {
            return this.gerichtsnameField;
        }
        set {
            this.gerichtsnameField = value;
        }
    }
    
    /// <remarks/>
    public string Datum {
        get {
            return this.datumField;
        }
        set {
            this.datumField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahren {
    
    private sbyte lfdnrField;
    
    private sbyte kammerField;
    
    private string sitzungssaalField;
    
    private string uhrzeitField;
    
    private string statusField;
    
    private string oeffentlichField;
    
    private string artField;
    
    private string azField;
    
    private string gegenstandField;
    
    private string bemerkung1Field;
    
    private string bemerkung2Field;
    
    private string[] besetzungField;
    
    private TerminsaushangVerfahrenAktivPartei aktivParteiField;
    
    private TerminsaushangVerfahrenPassivPartei passivParteiField;
    
    private TerminsaushangVerfahrenBeigeladen beigeladenField;
    
    private TerminsaushangVerfahrenSV svField;
    
    private TerminsaushangVerfahrenZeugen zeugenField;
    
    /// <remarks/>
    public sbyte Lfdnr {
        get {
            return this.lfdnrField;
        }
        set {
            this.lfdnrField = value;
        }
    }
    
    /// <remarks/>
    public sbyte Kammer {
        get {
            return this.kammerField;
        }
        set {
            this.kammerField = value;
        }
    }
    
    /// <remarks/>
    public string Sitzungssaal {
        get {
            return this.sitzungssaalField;
        }
        set {
            this.sitzungssaalField = value;
        }
    }
    
    /// <remarks/>
    public string Uhrzeit {
        get {
            return this.uhrzeitField;
        }
        set {
            this.uhrzeitField = value;
        }
    }
    
    /// <remarks/>
    public string Status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    public string Oeffentlich {
        get {
            return this.oeffentlichField;
        }
        set {
            this.oeffentlichField = value;
        }
    }
    
    /// <remarks/>
    public string Art {
        get {
            return this.artField;
        }
        set {
            this.artField = value;
        }
    }
    
    /// <remarks/>
    public string Az {
        get {
            return this.azField;
        }
        set {
            this.azField = value;
        }
    }
    
    /// <remarks/>
    public string Gegenstand {
        get {
            return this.gegenstandField;
        }
        set {
            this.gegenstandField = value;
        }
    }
    
    /// <remarks/>
    public string Bemerkung1 {
        get {
            return this.bemerkung1Field;
        }
        set {
            this.bemerkung1Field = value;
        }
    }
    
    /// <remarks/>
    public string Bemerkung2 {
        get {
            return this.bemerkung2Field;
        }
        set {
            this.bemerkung2Field = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Richter", IsNullable=false)]
    public string[] Besetzung {
        get {
            return this.besetzungField;
        }
        set {
            this.besetzungField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenAktivPartei AktivPartei {
        get {
            return this.aktivParteiField;
        }
        set {
            this.aktivParteiField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenPassivPartei PassivPartei {
        get {
            return this.passivParteiField;
        }
        set {
            this.passivParteiField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenBeigeladen Beigeladen {
        get {
            return this.beigeladenField;
        }
        set {
            this.beigeladenField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenSV SV {
        get {
            return this.svField;
        }
        set {
            this.svField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenZeugen Zeugen {
        get {
            return this.zeugenField;
        }
        set {
            this.zeugenField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenAktivPartei {
    
    private string[] parteienField;
    
    private TerminsaushangVerfahrenAktivParteiProzBev prozBevField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Partei", IsNullable=false)]
    public string[] Parteien {
        get {
            return this.parteienField;
        }
        set {
            this.parteienField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenAktivParteiProzBev ProzBev {
        get {
            return this.prozBevField;
        }
        set {
            this.prozBevField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenAktivParteiProzBev {
    
    private string[] pbField;
    
    private string[] textField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PB")]
    public string[] PB {
        get {
            return this.pbField;
        }
        set {
            this.pbField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenPassivPartei {
    
    private string[] parteienField;
    
    private TerminsaushangVerfahrenPassivParteiProzBev prozBevField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Partei", IsNullable=false)]
    public string[] Parteien {
        get {
            return this.parteienField;
        }
        set {
            this.parteienField = value;
        }
    }
    
    /// <remarks/>
    public TerminsaushangVerfahrenPassivParteiProzBev ProzBev {
        get {
            return this.prozBevField;
        }
        set {
            this.prozBevField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenPassivParteiProzBev {
    
    private string[] pbField;
    
    private string[] textField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PB")]
    public string[] PB {
        get {
            return this.pbField;
        }
        set {
            this.pbField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenBeigeladen {
    
    private string[] parteienField;
    
    private string[] prozBevField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Partei", IsNullable=false)]
    public string[] Parteien {
        get {
            return this.parteienField;
        }
        set {
            this.parteienField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("PB", IsNullable=false)]
    public string[] ProzBev {
        get {
            return this.prozBevField;
        }
        set {
            this.prozBevField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenSV {
    
    private string[] parteienField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Partei", IsNullable=false)]
    public string[] Parteien {
        get {
            return this.parteienField;
        }
        set {
            this.parteienField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class TerminsaushangVerfahrenZeugen {
    
    private string[] parteienField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Partei", IsNullable=false)]
    public string[] Parteien {
        get {
            return this.parteienField;
        }
        set {
            this.parteienField = value;
        }
    }
}
