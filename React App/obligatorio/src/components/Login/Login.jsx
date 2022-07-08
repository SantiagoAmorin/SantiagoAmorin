import { useRef } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { userLogin, LOCAL_STORAGE_KEY } from '../../services/api';
import { onLoginAction } from '../../store/actions';
import './Login.css';

const Login = () => {
  const usernameRef = useRef()
  const passwordRef = useRef()
  const user = useSelector(state => state.user)
  const dispatch = useDispatch()

  const handleSubmit = e => {
    e.preventDefault()

    if (usernameRef.current.value === '') {//validaciones
      alert('Debes completar el campo username')
    } else if (passwordRef.current.value === '') {
      alert('Debes completar el campo password')
    } else {
      userLogin({
        usuario: usernameRef.current.value,
        password: passwordRef.current.value
      })
        .then(userData => {
          dispatch(onLoginAction(userData))
          localStorage.setItem(LOCAL_STORAGE_KEY, userData.apiKey);
        })
        .catch(statusError => {
          if (statusError === 404) {
            alert('Datos incorrectos, el usuario no existe')
          } else {
            alert('Algo salió mal, por favor reintenta más tarde.')
          }
        })
    }
  }
  return (
    <>
      <section className='d-flex flex-md justify-content-center login'>
        <div className='card'>
          <h2>Bienvenido nuevamente!</h2>
          <section className='card-body'>
            {user ? (
              <div className='alert alert-success' role='alert'>
                Inicio de session correcto
              </div>
            ) : (
              ''
            )}
            <form>
              <label htmlFor='inputEmail'>Username</label>
              <br />
              <input
                type='email'
                name='username'
                className='form-control'
                ref={usernameRef}
              />
              <br />
              <label htmlFor='inputPassword'>Password</label>
              <br />
              <input
                type='password'
                name='password'
                className='form-control'
                ref={passwordRef}
              />
              <br />
              <button className='btn btn-primary' onClick={handleSubmit}>
                Login
              </button>
              <br />
              <br />
              <p>¿No tienes cuenta?</p>
              <Link to='/signup'>Registrate</Link>
            </form>
          </section>
        </div>
      </section>
    </>
  )
}

export default Login
