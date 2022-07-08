import { useRef, useState } from 'react';
import './Venta.css';

const Venta = ({ paquetes, comprarPaquete, idUser }) => {
  const idPaqueteRef = useRef();
  const nombreRef = useRef();
  const cantMenRef = useRef();
  const cantMayRef = useRef();

  const onCompraClick = async e =>{
    e.preventDefault()
    if (nombreRef.current.value != '' || cantMayRef.current.value + cantMenRef.current.value != '') {//validaciones
      if (Number(cantMayRef.current.value) > 0 || Number(cantMenRef.current.value) > 0) {
        if (Number(cantMayRef.current.value) >= 0 && Number(cantMenRef.current.value) >= 0) {
          if (Number(cantMayRef.current.value) + Number(cantMenRef.current.value) <= 10){
            if (Number.isInteger(Number(cantMayRef.current.value)) && Number.isInteger(Number(cantMenRef.current.value))) {
              try {
                await comprarPaquete({
                  idVendedor: idUser,
                  nombreCliente: nombreRef.current.value,
                  idPaquete: Number(idPaqueteRef.current.value),
                  cantidadMayores: Number(cantMayRef.current.value),
                  cantidadMenores: Number(cantMenRef.current.value)                    
                });
                nombreRef.current.value = '';//borro los campos. No es la mejor solucion
                cantMayRef.current.value = '';
                cantMenRef.current.value = '';
              }
              catch (error) {
                alert(error);
              }
            }
            else {
              alert("La cantidad de personas debe ser un número entero");
            }
          }
          else{
              alert("La cantidad de personas sumadas no puede ser mayor a 10");
          }
        }
        else {
          alert("La cantidad de personas debe ser positiva");
        }
      }
      else {
        alert("Ingrese una cantidad de personas");
      }
    }
    else {
      alert("No puede haber campos vacíos");
    }
  }

  return (
    <section className='d-flex flex-md justify-content-center venta'>
      <div className='card'>
        <h2>Vender Paquete</h2>
        <section className='card-body'>
          <form>
            <label htmlFor="txtNombre">Ingrese el nombre del cliente</label>
              <br />
              <input type="text" id="txtNombre" className='form-control' ref={nombreRef}/>
              <br />
              <label htmlFor="slcPaquete">Seleccione el paquete</label>
              <br />
              <select id="slcPaquete" className='form-control' ref={idPaqueteRef}>
                {paquetes.map(({id, nombre}) =>(
                  <option value={`${id}`}>{nombre}</option>
                ))}
              </select>
              <br />
              <label htmlFor="cantMayores">Ingrese cantidad de mayores</label>
              <br />
              <input type="number" min="0" id="cantMayores" className='form-control' ref={cantMayRef}/>
              <br />
              <label htmlFor="cantMenores">Ingrese cantidad de menores</label>
              <br />
              <input type="number" min="0" id="cantMenores" className='form-control' ref={cantMenRef}/>
              <br />
              <button className='btn btn-primary' onClick={onCompraClick}>Comprar</button>
          </form>
        </section>
      </div>
    </section>
  )
}
  
export default Venta;