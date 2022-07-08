import 'bootstrap-css-only';
import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { Route, Switch, useHistory } from 'react-router-dom';
import './App.css';
import Dashboard from './components/Dashboard/Dashboard';
import Header from './components/Header/Header';
import Login from './components/Login/Login';
import PrivateRoute from './components/PrivateRoute';
import SignUp from './components/Signup/Signup';

function App() {
  const userLogged = useSelector((state) => state.user);
  const history = useHistory();

  useEffect(() => {
    if (userLogged !== null) {
      history.push('/dashboard');
    }
  }, [userLogged]);

  return (
    <main className="App">
      <Header />
      <Switch>
        <Route path="/" exact>
          <Login />
        </Route>
        <Route path="/login" exact>
          <Login />
        </Route>
        <Route path="/signup" exact>
          <SignUp />
        </Route>
        <PrivateRoute path="/dashboard" component={Dashboard} />
      </Switch>
    </main>
  );
}

export default App;