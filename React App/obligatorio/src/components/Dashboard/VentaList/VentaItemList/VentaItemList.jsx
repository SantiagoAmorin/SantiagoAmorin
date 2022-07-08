const VentaItemList = ({  id, cliente, paquete, cantAdultos, cantNinos, idPaquete, paquetes }) => {

  const calcularPrecio = () => {
    let precioTotal = 0;

    const paquete = paquetes.find(p => p.id == idPaquete);
    if(paquete) {
      precioTotal = paquete.precio_menor * cantNinos + paquete.precio_mayor * cantAdultos;
    }
    return precioTotal
  }

  return (
    <tr>
      <th scope='row'>{id}</th>
      <td>{cliente}</td>
      <td>{paquete}</td>
      <td>{cantAdultos}</td>
      <td>{cantNinos}</td>
      <td>{calcularPrecio()}</td>
    </tr>
  )
}
export default VentaItemList
