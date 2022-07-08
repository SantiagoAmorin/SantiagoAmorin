import '../Stats/Metrics/Metrics.css'

const CantVentas = ({ ventasHechas }) => {
  return (
    <div className='row'>
      <div className='col-sm-6'>
        <div className='card'>
          <div className='card-body'>
            <h5 className='card-title'>Ventas Totales</h5>
            <p className='card-text'>
              <span class='badge bg-success'>{ventasHechas}</span>
            </p>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CantVentas