//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace vadimRabotaet.DALandDomain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Клиент
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиент()
        {
            this.Заказ = new HashSet<Заказ>();
        }
    
        public string Логин { get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public string Отчество { get; set; }
        public string Телефон { get; set; }
        public string Почта { get; set; }
        public string Пароль { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказ> Заказ { get; set; }
    }
}
