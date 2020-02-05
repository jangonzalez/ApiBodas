using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bodas.Model.BD
{
    [MetadataType(typeof(MetaC_Hoteles))]
    public partial class C_Hoteles { }
    public class MetaC_Hoteles
    {
        public int id { get; set; }
        public string name { get; set; }
        public string codigo { get; set; }
        public string empresa { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<C_Lugares> C_Lugares { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<C_Paquetes> C_Paquetes { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<C_Preguntas> C_Preguntas { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<C_Servicios> C_Servicios { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<C_tiposEvento> C_tiposEvento { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<C_Horarios> C_Horarios {get; set;}
    }
}
