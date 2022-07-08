using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Dominio.SeguimientoVacunas
{
	[Table("Usuarios")]
	public class Usuario
	{
		[Key]
		public string cedulaId { get; set; }

		[Required]
		public string contrasena { get; set; }

		public bool validar()
		{
			return verificarCedula(this.cedulaId);
		}

		public bool verificarCedula(string ci)
		{
			bool res = false;
			if (cedulaId.Length == 8)
			{
				for (int c = 0; c < ci.Length; c++)
				{
					if (Char.IsDigit(ci[c]))
					{
						res = true;
					}
					else
					{
						res = false;
					}
				}
			}
			return res;
		}

		public string crearContrasena(string ci)
		{
			string contrasena="";
			
			for(int i = 0; i <4; i++)
            {
				contrasena = contrasena + ci[i];
            }
			string dia= DateTime.Now.ToString("dddd", new CultureInfo("es-ES"));
			dia = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(dia));
			contrasena = contrasena + "-" + dia;
			return contrasena;
		}

		public bool validarContra(string contraseña)
		{
			bool retorno = false;
			int may = 0, min = 0, dig = 0;
			if (contraseña.Length >= 6)
			{
				for (int i = 0; i < contraseña.Length; i++)
				{
					if (Char.IsUpper(contraseña[i]))
					{
						may += 1;
					}
					else if (Char.IsLower(contraseña[i]))
					{
						min += 1;
					}
					else if (Char.IsDigit(contraseña[i]))
					{
						dig += 1;
					}
				}
				if (may >= 1 && min >= 1 && dig >= 1)
				{
					retorno = true;
				}
			}
			return retorno;
		}

		public bool esPrimerInicio()
        {
			string contra = this.contrasena;
			string contraInicio= "";
			for (int i = 0; i < 4; i++)
                {
                    contraInicio = contraInicio + this.cedulaId[i];
                }
			string contrAux = "";
			for (int i = 0; i < 4; i++)
                {
                    contrAux = contrAux + contra[i];
                }
			if (contraInicio == contrAux)
            {
				return true;
            }
            else
            {
				return false;
            }
        }

		public string Encriptar()
		{
			string _cadenaAencriptar = this.contrasena;
			string result = string.Empty;
			byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
			result = Convert.ToBase64String(encryted);
			return result;
		}

		public string desEncriptar(string _cadenaAdesencriptar)
		{
			string result = string.Empty;
			byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
			//result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
			result = System.Text.Encoding.Unicode.GetString(decryted);
			return result;
		}


	}

}

