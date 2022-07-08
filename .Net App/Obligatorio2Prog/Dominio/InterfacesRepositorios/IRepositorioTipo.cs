using Dominio.SeguimientoVacunas;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioTipo
	{
		bool Add(Tipo unTipo);
		IEnumerable<Tipo> FindAll();

		Tipo FindByCode(string codigo);

	}

}

