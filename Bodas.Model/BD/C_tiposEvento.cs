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
    
    public partial class C_tiposEvento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_tiposEvento()
        {
            this.Agenda = new HashSet<Agenda>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public int idHotel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        public virtual C_Hoteles C_Hoteles { get; set; }
    }
}
