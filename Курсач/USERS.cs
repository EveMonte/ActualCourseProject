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
    
    public partial class USERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERS()
        {
            this.BASKETS = new HashSet<BASKETS>();
            this.MARKS = new HashSet<MARKS>();
            this.YOUR_BOOKS = new HashSet<YOUR_BOOKS>();
        }
    
        public int USER_ID { get; set; }
        public string EMAIL { get; set; }
        public string SECOND_NAME { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string ACCOUNT { get; set; }
        public string CREDIT_CARD { get; set; }
        public Nullable<int> SUBSCRIPTION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BASKETS> BASKETS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MARKS> MARKS { get; set; }
        public virtual SUBSCRIPTIONS SUBSCRIPTIONS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YOUR_BOOKS> YOUR_BOOKS { get; set; }
    }
}
