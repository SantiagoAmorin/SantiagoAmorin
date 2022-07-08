using Dominio.SeguimientoVacunas;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioLaboratorio
	{

		bool Add(Laboratorio unLaboratorio);
		IEnumerable<Laboratorio> FindAll();

		IEnumerable<Laboratorio> FindByNames(List<string> laboratorios);

		Laboratorio FindByName(string laboratorio);

	}

}

