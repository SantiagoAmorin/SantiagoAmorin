import { ON_LOGIN, ON_LOGOUT } from './constants';

const onLoginAction = (user) => ({
  type: ON_LOGIN,
  payload: {
    user: user,
  },
});


const onLogoutAction = (user) => ({
  type: ON_LOGOUT,
});

export { onLoginAction, onLogoutAction };
