//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bodas.Model.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_Paquetes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_Paquetes()
        {
            this.Agenda = new HashSet<Agenda>();
            this.paqueteServicio = new HashSet<paqueteServicio>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string descripcion { get; set; }
        public int idHotel { get; set; }
        public string clave { get; set; }
        public decimal total { get; set; }
        public int idMoneda { get; set; }
        public bool status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        public virtual C_Hoteles C_Hoteles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<paqueteServicio> paqueteServicio { get; set; }
    }
}
