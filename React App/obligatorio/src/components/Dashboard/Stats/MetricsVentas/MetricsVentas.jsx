import './MetricsVentas.css'

const MetricsVentas = ({ completed }) => {
  return (
    <div className='row metrics-ventas'>
      <div className='col-sm-6'>
        <div className='card'>
          <div className='card-body'>
            <h5 className='card-title'>Cantidad de Ventas</h5>
            <p className='card-text'>
              <span class='badge bg-success'>{completed}</span>
            </p>
          </div>
        </div>
      </div>
    </div>
  )
}

export default MetricsVentas
