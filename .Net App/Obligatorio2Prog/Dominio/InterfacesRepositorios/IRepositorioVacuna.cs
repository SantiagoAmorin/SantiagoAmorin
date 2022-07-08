using Dominio.SeguimientoVacunas;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioVacuna
	{
		IEnumerable<Vacuna> FindAll();

		Vacuna FindById(int Id);

		IEnumerable<Vacuna> FindByAprob(int faseAprob);

		IEnumerable<Vacuna> FindByLab(string laboratorio);

		bool AddVacuna(Vacuna unaVacuna);

		IEnumerable<Vacuna> filtrarVacunas(int? faseDada, int? precioMin, int? precioMax, string tipo, string laboratorio, string pais);
		IEnumerable<Vacuna> filtrarVacunas2(int? faseDada, int? precioMin, int? precioMax, string tipo, string laboratorio, string pais);



	}

}

