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
    
    public partial class TeachersSchools
    {
        public int Id { get; set; }
        public int TeachersId { get; set; }
        public int SchoolsId { get; set; }
    
        public virtual Teachers Teachers { get; set; }
        public virtual Schools Schools { get; set; }
    }
}