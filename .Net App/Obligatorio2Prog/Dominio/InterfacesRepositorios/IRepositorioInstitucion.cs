using Dominio.SeguimientoVacunas;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioInstitucion
	{
		bool Add(Institucion unaInstitucion);

		Institucion FindById(int id);

	}

}

