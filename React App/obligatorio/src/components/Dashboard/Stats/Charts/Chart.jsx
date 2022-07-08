import { useEffect, useState } from 'react'
import Chart from 'react-apexcharts'

const BarChart = ({ destinos, cantPasajeros }) => {
  const [barData, setData] = useState({
    options: {
      chart: {
        id: 'basic-bar'
      },
      xaxis: {
        categories: destinos
      }
    },
    series: [
      {
        name: 'series-1',
        data: cantPasajeros
      }
    ]
  })
  useEffect(() => {
    setData({
      options: {
        chart: {
          id: 'basic-bar'
        },
        xaxis: {
          categories: destinos
        }
      },
      series: [
        {
          name: 'series-1',
          data: cantPasajeros
        }
      ]
    })
  }, cantPasajeros)

  return (
    <Chart
      type='bar'
      width='500'
      options={barData.options}
      series={barData.series}
    />
  )
}
export default BarChart