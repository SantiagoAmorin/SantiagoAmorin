using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportadoArchivos
{
    public class ManejoArchivos
    {
		private static string raiz = AppDomain.CurrentDomain.BaseDirectory + @"ArchivosImportados";
		private static string archivoVacunas = "VacunasGuardadas.txt";
		private static string archivoLaboratorios = "LaboratoriosGuardados.txt";
		private static string archivoTipos = "TiposGuardados.txt";
		private static string archivoUsuarios = "UsuariosGuardadas.txt";
		private static string archivoLaboratoriosEnVac = "LabEnVacGuardadas.txt";
		private static string archivoPaisEnVac = "PaisEnVacGuardadas.txt";

		public bool importarDatos()
        {
			return LeerArchivoLaboratorios()
				&& LeerArchivoTipos()
				&& LeerArchivoUsuarios()
				&& LeerArchivoVacunas();
        }

		public bool LeerArchivoLaboratorios()
		{
			RepositorioLaboratorio repoLaboratorio = new RepositorioLaboratorio();
			
            try
            {
				string dirFinal = Path.Combine(raiz, archivoLaboratorios);
				using (StreamReader sr = new StreamReader(dirFinal))
				{
					
					string linea = sr.ReadLine();
					while (linea != null)
					{
						string[] words = linea.Split('|');
						
                        
							if (repoLaboratorio.FindByName(words[1].ToString()) == null)
							{
								repoLaboratorio.Add(new Laboratorio()
								{
									Id = Int32.Parse(words[0].ToString()),
									nombre = words[1].ToString(),
									pais = words[2].ToString(),
									experiencia = Boolean.Parse(words[3].ToString())
								});
							}

						linea = sr.ReadLine();
                        
					}
				}
				return true;
			}catch(Exception e)
            {
				return false;
            }
			
		}

		public bool LeerArchivoTipos()
		{
			RepositorioTipo repoTipo = new RepositorioTipo();
			
            try
            {
				using (StreamReader sr = new StreamReader(Path.Combine(raiz, archivoTipos)))
				{
					string linea = sr.ReadLine();
					while (linea != null)
					{
						string[] words = linea.Split('|');
                        if (repoTipo.FindByCode(words[1].ToString()) == null)
                        {
							repoTipo.Add(new Tipo()
							{
								nombre = words[0].ToString(),
								codigoId = words[1].ToString(),
								descripcion = words[2].ToString()
							});
						}
						linea = sr.ReadLine();
					}
				}
				return true;
			}
            catch(Exception e)
            {
				return false;
            }
			
		}

		public bool LeerArchivoUsuarios()
		{
            try
            {
				RepositorioUsuario repoUsuario = new RepositorioUsuario();
				using (StreamReader sr = new StreamReader(Path.Combine(raiz, archivoUsuarios)))
				{
					string linea = sr.ReadLine();
					while (linea != null)
					{
						string[] words = linea.Split('|');
						if (repoUsuario.FindbyCI(words[0].ToString()) == null)
						{
							Usuario user = new Usuario();
							user.cedulaId = words[0].ToString();
							user.contrasena = user.crearContrasena(words[0].ToString());
							repoUsuario.Add(user);
						}
						linea = sr.ReadLine();
					}
				}
				return true;
			}
			catch (Exception err)
            {
				return false;
            }
			
		}

	
		public bool LeerArchivoVacunas()
		{
			RepositorioVacuna repoVacuna = new RepositorioVacuna();
			RepositorioLaboratorio repoLab = new RepositorioLaboratorio();
			RepositorioUsuario repoUsuario = new RepositorioUsuario();
			RepositorioTipo repoTipo = new RepositorioTipo();
			bool ret = false;
			try {
				using (StreamReader sr = new StreamReader(Path.Combine(raiz, archivoVacunas)))
				{
					string linea = sr.ReadLine();
					while (linea != null)
					{
						string[] words = linea.Split('|');
                        if (repoVacuna.FindById(Int32.Parse(words[0].ToString())) == null)
                        {
							Vacuna vac = new Vacuna()
							{
								Id = Int32.Parse(words[0].ToString()),
								nombre = words[1].ToString(),
								precio = Int32.Parse(words[2].ToString()),
								cantDosis = Int32.Parse(words[3].ToString()),
								plazoDosis = Int32.Parse(words[4].ToString()),
								tempMin = Int32.Parse(words[5].ToString()),
								tempMax = Int32.Parse(words[6].ToString()),
								fechActua = DateTime.Parse(words[7].ToString()),
								mecanismoCOVAX = Boolean.Parse(words[8].ToString()),
								efectosAdvers = words[9].ToString(),
								aprovEmerg = Boolean.Parse(words[10].ToString()),
								faseClinica = Int32.Parse(words[13].ToString()),
								prevHosp = Int32.Parse(words[14].ToString()),
								prevCTI = Int32.Parse(words[15].ToString()),
								prevCovid = Int32.Parse(words[16].ToString()),
								edadMin = Int32.Parse(words[17].ToString()),
								edadMax = Int32.Parse(words[18].ToString()),
								produAnual=Int32.Parse(words[19].ToString())
							};
							vac.idTipo = words[11].ToString();
							vac.idUsuario = words[12].ToString();
							vac.Usuario = repoUsuario.FindbyCI(words[12].ToString());
							vac.miTipo = repoTipo.FindByCode(words[11].ToString());
							vac.laboratorio = new List<Laboratorio>();
							
							using (StreamReader sl = new StreamReader(Path.Combine(raiz, archivoLaboratoriosEnVac)))
							{
								string linea2 = sl.ReadLine();
								while (linea2 != null)
								{
									string[] labs = linea2.Split('|');
                                    if (vac.Id == Int32.Parse(labs[0].ToString()))
                                    {
										
										Laboratorio lab = repoLab.FindByName(labs[1].ToString());
                                        if (lab != null)
                                        {
											vac.agregarLaboratorio(lab);
                                        }


									}
									linea2 = sl.ReadLine();
								}
							}
							using (StreamReader sp = new StreamReader(Path.Combine(raiz, archivoPaisEnVac)))
							{
								string linea3 = sp.ReadLine();
								while (linea3 != null)
								{
									string[] status = linea3.Split('|');
									if (vac.Id == Int32.Parse(status[0].ToString()))
									{
										vac.status = vac.status + "," + status[1].ToString();
									}
									linea3 = sp.ReadLine();
								}
							}
							if (repoVacuna.AddVacuna(vac))
							{
								ret = true;
							}
						}
						linea = sr.ReadLine();
					}
				}
				return ret;
			}
			catch(Exception e)
            {
				return ret;

            }
		}
	}
}
