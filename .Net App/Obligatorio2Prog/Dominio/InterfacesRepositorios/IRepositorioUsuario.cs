using Dominio.SeguimientoVacunas;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioUsuario
	{
		IEnumerable<Usuario> FindAll();

		Usuario FindbyCI(string ci);

		bool Add(Usuario unUsuario);

		Usuario updateContra(Usuario usr);

	}

}

