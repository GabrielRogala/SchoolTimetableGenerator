//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STG_website.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubGroups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubGroups()
        {
            this.Lessons = new HashSet<Lessons>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupsId { get; set; }
        public int SchoolsId { get; set; }
        public string Number { get; set; }
    
        public virtual Groups Groups { get; set; }
        public virtual Schools Schools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
