//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SparesTracker
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;


    public partial class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public int warehouseId { get; set; }

        public virtual Roles Roles { get; set; }
        public virtual Warehouses Warehouses { get; set; }
    }
}