import { useRef, useState } from 'react'
import { useHistory } from 'react-router-dom'
import { register } from '../../services/api'

const SignUp = () => {
  const userRef = useRef()
  const passRef = useRef()

  const history = useHistory()
  const [isAlertVisible, setAlertVisibillity] = useState(false)
  const [message, setMessage] = useState('')
  const [alertClass, setAlertClass] = useState('')

  const onHandleSubmit = async e => {
    e.preventDefault()

    let wasSuccess = false

    if (userRef.current.value !== '' && passRef.current.value !== '') {//validaciones
      try {
        await register({
          usuario: userRef.current.value,
          password: passRef.current.value
        })
        setMessage('Registro con éxito')
        setAlertClass('success')
        setAlertVisibillity(true)

        wasSuccess = true
      } catch (error) {
        setMessage(error.message)
        setAlertClass('danger')
        setAlertVisibillity(true)
        wasSuccess = false
      }
    } else {
      setMessage('Debes completar todos los campos')
      setAlertClass('danger')
      setAlertVisibillity(true)
      wasSuccess = false
    }
    if (!wasSuccess) {
      setTimeout(() => {
        setMessage('')
        setAlertClass('')
        setAlertVisibillity(false)
      }, 3000)
    } else {
      setTimeout(() => {
        history.push('/login')
      }, 3000)
    }
  }
  return (
    <>
      <section className='d-flex flex-md justify-content-center login'>
        <div className='card'>
          <h2>Registro</h2>
          <section className='card-body'>
            {isAlertVisible ? (
              <div className={`alert alert-${alertClass}`} role='alert'>
                {message}
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
                ref={userRef}
              />
              <br />
              <label htmlFor='inputPassword'>Password</label>
              <br />
              <input
                type='password'
                name='password'
                className='form-control'
                ref={passRef}
              />
              <br />
              <button className='btn btn-danger' onClick={onHandleSubmit}>
                Registrarse
              </button>
            </form>
          </section>
        </div>
      </section>
    </>
  )
}

export default SignUp
