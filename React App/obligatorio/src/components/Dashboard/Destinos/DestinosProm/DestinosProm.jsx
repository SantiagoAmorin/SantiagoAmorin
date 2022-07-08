import './DestinosProm.css';

const DestinosProm = ({ destinosProm }) => {

  return (
    <section className='d-flex flex-md justify-content-center destinos-top'>
      <h3>Destinos a Promocionar</h3>
      <table className='table'>
        <thead>
          <tr>
            <th>Destino</th>
          </tr>
        </thead>
        <tbody>
          {destinosProm.map(({nombre}) => (
            <tr>
              <td>{nombre}</td>
            </tr>
            ))
          }
        </tbody>
      </table>
    </section>
  )
}

export default DestinosProm