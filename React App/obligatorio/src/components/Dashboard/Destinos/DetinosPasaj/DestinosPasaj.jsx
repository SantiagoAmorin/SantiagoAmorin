import DestinosPasajItem from "./DetinosPasajItem/DestinosPasajItem";

const DestinosPasaj = ({ paquetes, cantPasaj }) => {
  return (
    <section className='d-flex flex-md justify-content-center ventas-list'>
      <table className='table'>
        <thead>
          <tr>
            <th>Destino</th>
            <th>Cantidad de Personas</th>       
          </tr>
        </thead>
        <tbody>            
          { paquetes.map(function(object, i){
              return (<DestinosPasajItem paquete={object} clientes={cantPasaj[i]} />)
            })   
          }   
        </tbody>
      </table>
    </section>
  )
}
  
export default DestinosPasaj