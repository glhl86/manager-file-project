using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrossCutting.ApiModel
{
    public class PersonAM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string Identification { get; set; }
        public string Cellphone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long? IdState { get; set; }
        public virtual StatesAM States { get; set; }
    }
}
