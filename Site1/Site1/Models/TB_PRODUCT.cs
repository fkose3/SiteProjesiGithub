//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Site1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_PRODUCT
    {
        public int ID { get; set; }
        public int m_iBrand { get; set; }
        public int m_iCategory { get; set; }
        public string strTitle { get; set; }
        public string strDesc { get; set; }
        public string CoverImage { get; set; }
        public int m_iPrice { get; set; }
        public byte m_bPriceType { get; set; }
        public string Docs { get; set; }
        public string Stars { get; set; }
        public int m_iProductType { get; set; }
    }
}
