import VentaItemList from './VentaItemList/VentaItemList';
import './VentasList.css';

const VentasList = ({ ventas, paquetes }) => {
  return (
    <section className='d-flex flex-md justify-content-center ventas-list'>
      <h3>Lista de Paquetes Vendidos</h3>
      <table className='table'>
        <thead>
          <tr>
            <th>#</th>
            <th>Cliente</th>
            <th>Paquete</th>
            <th>Cant Adultos</th>
            <th>Cant Niños</th>
            <th>Precio Final</th>
          </tr>
        </thead>
        <tbody>
          {ventas.map(({ id, nombre_cliente, id_paquete, cantidad_mayores, cantidad_menores }) => (
            <VentaItemList
              id={id}
              key={`venta-${id}`}
              cliente={nombre_cliente}
              paquete={id_paquete}
              cantAdultos={cantidad_mayores}
              cantNinos={cantidad_menores}
              idPaquete={id_paquete}
              paquetes={paquetes}
            />
          ))}
        </tbody>
      </table>
    </section>
  )
}

export default VentasList
