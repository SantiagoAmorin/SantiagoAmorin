import './DestinosTop.css';

const DestinosTop = ({ destinosTop }) => {

  return (
    <section className='d-flex flex-md justify-content-center destinos-top'>
      <h3>Destinos Top</h3>
      <table className='table'>
        <thead>
          <tr>
            <th>Destino</th>
            <th>Cant Ventas</th>
          </tr>
        </thead>
        <tbody>
          {destinosTop.map(({ destino}) => (
            <tr>
              <td>{destino}</td>
              <td>{/*cantVentas*/}</td>
            </tr>
            ))
          }
        </tbody>
      </table>
    </section>

  )
}

export default DestinosTop;