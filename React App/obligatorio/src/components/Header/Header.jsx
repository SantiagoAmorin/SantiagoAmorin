import { useDispatch, useSelector } from 'react-redux';
import { onLogoutAction } from '../../store/actions';
import './Header.css';
import logo from './logo.svg';
import { LOCAL_STORAGE_KEY } from '../../services/api';

const Header = () => {
  const dispatch = useDispatch();
  const user = useSelector(state => state.user);

  const onLogout = () => {
    localStorage.removeItem(LOCAL_STORAGE_KEY);//borro la api key del local storage
    dispatch(onLogoutAction())
  }
  return (
    <header className='App-header'>
      <nav className='navbar navbar-dark bg-dark'>
        <a className='navbar-brand' href='#'>
          <img
            src={logo}
            width='30'
            height='30'
            className='d-inline-block align-top'
            alt=''
          />
          Obligatorio
        </a>
        {user ? (
          <button onClick={onLogout} className='btn btn-info'>
            Logout
          </button>
        ) : (
          ''
        )}
      </nav>
    </header>
  )
}
export default Header
