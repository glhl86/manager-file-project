using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ApiModel
{
    public class RegisterAM
    {
        [Required(ErrorMessage = "Campo requerido.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Debe tener entre 7 y 15 caracteres")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[!@@#$\\_.%^&*])[a-zA-Z0-9!@@#$\\_.%^&*]{7,15}$", ErrorMessage = "* Debe contener al menos una letra mayúscula y una minúscula.<br>* Debe contener al menos un número.<br>* Debe contener un carácter especial.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        public string RolName { get; set; }

        public PersonAM Person { get; set; }
    }
}
