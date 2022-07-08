import React from 'react'
import { useSelector } from 'react-redux'
import { Redirect, Route } from 'react-router-dom'

const PrivateRoute = ({ component: Component, ...rest }) => {
  const user = useSelector(state => state.user)
  if (user) {
    return (
      <Route>
        <Component {...rest} user={user} />
      </Route>
    )
  }
  return (
    <Route>
      <Redirect to='/login' />
    </Route>
  )
}

export default PrivateRoute
