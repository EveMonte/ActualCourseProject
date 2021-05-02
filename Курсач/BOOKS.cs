//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Курсач
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class BOOKS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOKS()
        {
            this.BASKETS = new HashSet<BASKETS>();
            this.YOUR_BOOKS = new HashSet<YOUR_BOOKS>();
            this.MARKS = new HashSet<MARKS>();
        }
    
        public int BOOK_ID { get; set; }
        public string TITLE { get; set; }
        public string AUTHOR { get; set; }
        public Nullable<int> GENRE { get; set; }
        public int YEAR { get; set; }
        public decimal PRICE { get; set; }
        public string COVER { get; set; }
        public string CATEGORY { get; set; }
        public decimal RATING { get; set; }
        public int PAGES { get; set; }
        public string DESCRIPTION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BASKETS> BASKETS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YOUR_BOOKS> YOUR_BOOKS { get; set; }
        public virtual GENRES GENRES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MARKS> MARKS { get; set; }
        [NotMapped]
        public string Genre { get; set; }
        [NotMapped]
        public int Mark { get; set; }
        [NotMapped]
        public int NUMBEROFVOICES { get; set; }
    }
}
