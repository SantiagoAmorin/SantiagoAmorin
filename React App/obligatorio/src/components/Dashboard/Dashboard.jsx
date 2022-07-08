import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { getPaquetes, registrarCompra, getVentas } from '../../services/api';
import DestinosProm from './Destinos/DestinosProm/DestinosProm';
import DestinosTop from './Destinos/DestinosTop/DestinosTop';
import './Dashboard.css';
import BarChart from './Stats/Charts/Chart';
import MetricsVentas from './Stats/MetricsVentas/MetricsVentas';
import VentasList from './VentaList/VentasList';
import Venta from './Venta/Venta';
import { useDispatch } from 'react-redux';
import { onLogoutAction } from '../../store/actions';
import DestinosPasaj from './Destinos/DetinosPasaj/DestinosPasaj';
import 'bootstrap-css-only';

const Dashboard = () => {
  const dispatch = useDispatch();
  const [ventas, setVentas] = useState([])
  const [paquetes, setPaquetes] = useState([]);
  const [precios, setPrecios] = useState([]);
  const user = useSelector(state => state.user);

  useEffect(() => {
    ;(async () => {
      try {
        const response = await getVentas(user.id);
        setVentas(response.ventas);
      } catch (error) {
        alert(error);
      }
    })()
  }, [])

  useEffect(() => {
    ;(async () => {
      try {
        const response = await getPaquetes(user.id);
        setPaquetes(response.destinos);
      } catch (error) {
        alert(error);
      }
    })()
  }, [])

  const comprarPaquete = paquete => {
    const response =  registrarCompra(paquete)
    .then(response => {
      if (response.codigo === 200) {
        alert('Venta Realizada!');
      }
      else if (response === 401) {
        dispatch(onLogoutAction());
        alert('Su sesión a expirado. Por favor inicie sesión nuevamente');
      }
    })
  }

  const filtrarTop = () => {
    let ventasTop = [];

    paquetes.map(p => {
      const cantidadVentas = calcularCantidadVentas(p.id)
      if(cantidadVentas > 3) {
      ventasTop.push(p)
      }
    })

  return ventasTop;
  }

  const calcularCantidadVentas = (idPaquete) => {
    const total = ventas.filter(v => v.id_paquete == idPaquete)
    return total.length
  }

  const filtrarProm = () => {
    let ventasProm = [];

    paquetes.map(p => {
      const cantidadVentas = calcularCantidadVentas(p.id)
      if(cantidadVentas == 0) {
        ventasProm.push(p)
      }
    })

    return ventasProm;
  }

  const perDest = () => {
    let destino = [];
    let cantPasaj = [];
    let cant =0;
    paquetes.forEach(paq => {
      ventas.forEach(ven =>{
        if(paq.id == ven.id_paquete){
          cant += ven.cantidad_menores + ven.cantidad_mayores;
          
        }
      })    
      destino.push(paq.nombre);      
      cantPasaj.push(cant);
      cant=0;
    })
    
     return {
       destinos : destino,
       pasajeros : cantPasaj
     }
   }

  const precioPromedio = () => {
    let destino = [];
    let preProm = [];
    let valor = 0;
    paquetes.forEach(paq => {
      valor = (paq.precio_menor + paq.precio_mayor) /  2                       
      destino.push(paq.nombre);      
      preProm.push(valor);
      valor=0;
    })

    return {
      destinos : destino,
      precios : preProm
    };
 }

 const cantVentas = () => {
  return ventas.length;
}

  return (
    <div className='container-fluid dashboard'>
      <h1>Dashboard</h1>
      <Venta paquetes={paquetes} comprarPaquete={comprarPaquete} idUser={user.id}/>
      <VentasList
        ventas={ventas}
        paquetes={paquetes}
      />
      <MetricsVentas completed={cantVentas()}/>
      <div>
        <div className='row col-12 chart justify-content-around'>
          <h4 className='col-12'>Personas por Destino</h4>
          <DestinosPasaj className='col-6' paquetes={perDest().destinos} cantPasaj={perDest().pasajeros}/>
          {perDest().destinos && perDest().destinos.length > 0 ? (<BarChart className='col-6' destinos={perDest().destinos} cantPasajeros={perDest().pasajeros} />) : (<h6>Cargando</h6>)}
        </div> 
        <div className='row col-5 chart'>
          <h4>Precios Destinos</h4>
          {precioPromedio().destinos && precioPromedio().destinos.length > 0 ? (<BarChart destinos={precioPromedio().destinos} cantPasajeros={precioPromedio().precios} />) : (<h6>Cargando</h6>)}
        </div>  
      </div>      
      <DestinosTop destinosTop={filtrarTop()}/>
      <DestinosProm destinosProm={filtrarProm()}/>
    </div>
  )
}

export default Dashboard
