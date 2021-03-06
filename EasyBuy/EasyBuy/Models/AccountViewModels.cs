﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyBuyCR.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Recordarme en este navegador")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme en este navegador")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser de al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no son iguales.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Lastname { get; set; }
    }

    public class CompanyRegisterViewModel {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser de al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no son iguales.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public string CompanyCity { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Teléfono")]
        public string CompanyPhoneNumber { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser de al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no son iguales.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }
}
