//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kursovay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shipp_history
    {
        public int id_ship_history { get; set; }
        public Nullable<int> id_order { get; set; }
        public Nullable<int> id_partner { get; set; }
        public Nullable<int> id_warehouse { get; set; }
        public Nullable<System.DateTime> receiving_date { get; set; }
        public Nullable<System.DateTime> send_date { get; set; }
        public Nullable<bool> send { get; set; }
    
        public virtual Orders Orders { get; set; }
        public virtual Orders Orders1 { get; set; }
        public virtual Partners Partners { get; set; }
        public virtual Warehouses Warehouses { get; set; }
    }
}
