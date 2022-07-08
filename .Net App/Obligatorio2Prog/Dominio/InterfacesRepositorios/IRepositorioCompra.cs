using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System.Collections.Generic;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioCompra
	{
		IEnumerable<Compra> FindAll();

		bool Add(Compra unaCompra);

		IEnumerable<Compra> FindByInst(int regInst);

	}

}

